using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain
{
    public abstract class Entity<TId>
    {
        private List<BusinessRule> _violatedRules = new List<BusinessRule>();
        public TId Id { get; set; }

        protected abstract void Validate();
        public IEnumerable<BusinessRule> GetViolatedRules()
        {
            _violatedRules.Clear();
            Validate();
            return _violatedRules;
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _violatedRules.Add(businessRule);
        }

        public override bool Equals(object entity)
        {
            return entity != null && entity is Entity<TId> && this == (Entity<TId>)entity;
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}