﻿using ShopNC.Entity.Mapping;
using ShopNC.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ShopNC.Repository
{
    public class EFContextFactory : IDBContextFactory
    {
        private readonly object o = new object();
        static DbContext dbContext = null;
        public  DbContext GetCurrentContextInstence()
        {

            dbContext = (DbContext)CallContext.GetData(typeof(EFContextFactory).FullName);

            //首次加载线程槽无数据
            if (dbContext == null)
            {
                lock (o)
                {
                    if (dbContext == null)
                    {
                        //获取实例把实例放入线程槽中
                        dbContext = new ShopNCContext();
                        CallContext.SetData(typeof(EFContextFactory).FullName, dbContext);
                    }

                }
            }

           // dbContext = new ShopNCContext();
            return dbContext;
        }

        public  void DisposeContext()
        {
            if (dbContext!=null)
            {
                dbContext.Dispose();
            }
        }
    }
    
}
