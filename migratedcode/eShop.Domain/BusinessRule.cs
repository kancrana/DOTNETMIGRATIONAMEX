using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain
{
    public class BusinessRule
    {
        private string _property;
        private string _rule;
        public BusinessRule(string property, string rule)
        {
            this._property = property;
            this._rule = rule;
        }

        public string Property
        {
            get
            {
                return this._property;
            }

            set
            {
                this._property = value;
            }
        }

        public string Rule
        {
            get
            {
                return this._rule;
            }

            set
            {
                this._rule = value;
            }
        }
    }
}