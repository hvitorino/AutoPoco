using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Conventions;
using AutoPoco.Actions;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Engine
{
    public class GenerationContext : IGenerationContext
    {
        private readonly IGenerationConfiguration mObjectBuilders;
        private readonly IGenerationContextNode mNode;
        private readonly int mRecursionLimit;

        public IGenerationContextNode Node
        {
            get { return mNode; }
        }

        public int Depth { get; private set; }
        
        public IGenerationConfiguration Builders
        {
            get { return mObjectBuilders; }
        }

        public GenerationContext(IGenerationConfiguration objectBuilders)
            : this(objectBuilders, null)
        {

        }

        public GenerationContext(IGenerationConfiguration objectBuilders, IGenerationContextNode node)
        {
            mObjectBuilders = objectBuilders;
            this.mNode = node;
            CalculateDepth();
        }

        private void CalculateDepth()
        {
            var currentNode = mNode;
            int depth = 0;
            while(currentNode != null)
            {
                currentNode = FindNextTypeNode(currentNode);
                depth++;
            }
            this.Depth = depth;
        }

        private IGenerationContextNode FindNextTypeNode(IGenerationContextNode currentNode)
        {
            while(true)
            {
                currentNode = currentNode.Parent;
                if (currentNode == null || currentNode.ContextType == GenerationTargetTypes.Object) { break; }
            }
            return currentNode;
        }

        public virtual IObjectGenerator<TPoco> Single<TPoco>()
        {
            Type searchType = typeof(TPoco);
            IObjectBuilder foundType = mObjectBuilders.GetBuilderForType(searchType);
            return new ObjectGenerator<TPoco>(this, foundType);
        }

        public ICollectionContext<TPoco, IList<TPoco>> List<TPoco>(int count)
        {
            return new CollectionContext<TPoco, IList<TPoco>>(
               Enumerable.Range(0, count)
                    .Select(x=> this.Single<TPoco>()).ToArray()
               .AsEnumerable());
        }

        public TPoco Next<TPoco>()
        {
            return this.Single<TPoco>().Get();
        }

        public TPoco Next<TPoco>(Action<IObjectGenerator<TPoco>> cfg)
        {
            var generator = this.Single<TPoco>();
            cfg.Invoke(generator);
            return generator.Get();
        }

        public IEnumerable<TPoco> Collection<TPoco>(int count)
        {
            var generator = this.List<TPoco>(count);
            return generator.Get();
        }

        public IEnumerable<TPoco> Collection<TPoco>(int count, Action<ICollectionContext<TPoco, IList<TPoco>>> cfg)
        {
            var generator = this.List<TPoco>(count);
            cfg.Invoke(generator);
            return generator.Get();
        }

    }
}
