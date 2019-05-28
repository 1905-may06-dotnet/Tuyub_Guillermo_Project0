using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Crud
    {
        public sealed class DbInstance
        {
            private static pizzadb_gtContext instance = null;
            private DbInstance()
            {
            }
            public static pizzadb_gtContext Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new pizzadb_gtContext();
                        return instance;
                    }
                    else
                    {
                        return instance;
                    }
                }
            }

        }


    }

}