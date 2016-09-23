using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Web;
using Inventec.Models;
using EntityFramework.Extensions;
using System.Data.Entity.Infrastructure;
using System.Transactions;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Inventec.DAL
{
    public static class BaseDAL<T> where T : class
    {
        public static T Add(DbContext nContext, T entity)
        {
            nContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
            nContext.SaveChanges();
            return entity;
        }

        public static int Count(DbContext nContext, Expression<Func<T, bool>> predicate)
        {
            return nContext.Set<T>().Count(predicate);
        }

        public static bool Update(DbContext nContext, T entity)
        {
            nContext.Set<T>().Attach(entity);
            nContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            int count = nContext.SaveChanges();
            return count > 0;
        }

        public static bool Update(DbContext nContext, T entity, List<string> fileds)
        {
            if (entity != null && fileds != null)
            {
                nContext.Set<T>().Attach(entity);
                var SetEntry = ((IObjectContextAdapter)nContext).ObjectContext.
                    ObjectStateManager.GetObjectStateEntry(entity);
                foreach (var t in fileds)
                {
                    SetEntry.SetModifiedProperty(t);
                }
                int count = nContext.SaveChanges();
                return count > 0;
            }
            return false;
        }

        public static bool Delete(DbContext nContext, long? id)
        {
            T _entity = nContext.Set<T>().Find(id);
            nContext.Entry<T>(_entity).State = System.Data.Entity.EntityState.Deleted;
            return nContext.SaveChanges() > 0;
        }

        public static bool Delete(DbContext nContext, T entity)
        {
            nContext.Set<T>().Attach(entity);
            nContext.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            return nContext.SaveChanges() > 0;
        }

        public static bool Delete(DbContext nContext, Expression<Func<T, bool>> whereLambda)
        {
            return nContext.Set<T>().Where(whereLambda).Delete() > 0;
        }
        public static bool Exist(DbContext nContext, Expression<Func<T, bool>> anyLambda)
        {
            return nContext.Set<T>().Any(anyLambda);
        }
        public static T Find(DbContext nContext, long? id)
        {
            T _entity = nContext.Set<T>().Find(id);
            return _entity;
        }

        public static T Find(DbContext nContext, long? id, bool IsDetached)
        {
            T _entity = nContext.Set<T>().Find(id);
            if (_entity != null)
            {
                nContext.Entry<T>(_entity).State = System.Data.Entity.EntityState.Detached;
            }
            return _entity;
        }

        public static T Find(DbContext nContext, Expression<Func<T, bool>> whereLambda)
        {
            T _entity = nContext.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }

        public static T Find(DbContext nContext, Expression<Func<T, bool>> whereLambda, bool IsDetached)
        {
            T _entity = nContext.Set<T>().FirstOrDefault<T>(whereLambda);
            if (_entity != null)
            {
                nContext.Entry<T>(_entity).State = System.Data.Entity.EntityState.Detached;
            }
            return _entity;
        }

        public static IQueryable<T> FindList<S>(DbContext nContext, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = nContext.Set<T>().Where<T>(whereLamdba).AsNoTracking();
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).AsNoTracking();
            else _list = _list.OrderByDescending<T, S>(orderLamdba).AsNoTracking();
            return _list;
        }

        public static IQueryable<T> FindList<S>(DbContext nContext, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba, int topNum = 10)
        {
            var _list = nContext.Set<T>().Where<T>(whereLamdba).AsNoTracking();
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).AsNoTracking().Take(topNum);
            else _list = _list.OrderByDescending<T, S>(orderLamdba).AsNoTracking().Take(topNum);
            return _list;
        }

        public static IQueryable<T> FindPageList<S>(DbContext nContext, int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = nContext.Set<T>().Where<T>(whereLamdba).AsNoTracking();
            totalRecord = _list.Count();
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).AsNoTracking().Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            else _list = _list.OrderByDescending<T, S>(orderLamdba).AsNoTracking().Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            return _list;
        }


        public static T SaveData(DbContext db, T entity, HttpRequestBase request)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    db.Entry<T>(entity).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();                    

                    if (entity != null)
                    {
                        PropertyInfo[] fields = entity.GetType().GetProperties();
                        foreach (PropertyInfo ety in fields)
                        {
                            if (ety.PropertyType.Namespace == "System.Collections.Generic")
                            {
                                PropertyInfo[] dettailfields = ety.PropertyType.GenericTypeArguments[0].GetProperties(); 

                                if (request.Params.GetValues("item." + dettailfields[3].Name) != null)
                                {
                                    for (int i = 0; i < request.Params.GetValues("item." + dettailfields[3].Name).Length; i++)
                                    {
                                        if (!String.IsNullOrEmpty(request.Params.GetValues("item." + dettailfields[3].Name)[i]))
                                        {
                                            dynamic detailModel = (dynamic)ety.PropertyType.GenericTypeArguments[0];

                                            var detailEntity = detailModel.Assembly.CreateInstance(detailModel.FullName);

                                            foreach (PropertyInfo et in dettailfields)
                                            {
                                                if (et.Name != "id" && et.Name != "formid")
                                                {
                                                    if (request.Params.GetValues("item." + et.Name) != null && request.Params.GetValues("item." + et.Name).Length > i)
                                                    {
                                                        if ((et.PropertyType).GenericTypeArguments.Length > 0 && (et.PropertyType).GenericTypeArguments[0].Name == "DateTime")
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()))
                                                                et.SetValue(detailEntity, Convert.ToDateTime(request.Params.GetValues("item." + et.Name)[i]));
                                                        }
                                                        else if ((et.PropertyType).GenericTypeArguments.Length > 0 && (et.PropertyType).GenericTypeArguments[0].Name == "Decimal")
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()) && request.Params.GetValues("item." + et.Name)[i].ToString() != "null")
                                                                et.SetValue(detailEntity, Convert.ToDecimal(request.Params.GetValues("item." + et.Name)[i]));
                                                        }
                                                        else if (((et.PropertyType).GenericTypeArguments.Length > 0 && ((et.PropertyType).GenericTypeArguments[0].Name == "Int32")) || et.PropertyType.FullName=="System.Int32")
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()) && request.Params.GetValues("item." + et.Name)[i].ToString()!="null")
                                                                et.SetValue(detailEntity, Convert.ToInt32(request.Params.GetValues("item." + et.Name)[i]));
                                                        }
                                                        else if ((et.PropertyType).GenericTypeArguments.Length > 0 && ((et.PropertyType).GenericTypeArguments[0].Name == "Int64"))
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()) && request.Params.GetValues("item." + et.Name)[i].ToString() != "null")
                                                                et.SetValue(detailEntity, Convert.ToInt64(request.Params.GetValues("item." + et.Name)[i]));
                                                        }
                                                        else if ((et.PropertyType).GenericTypeArguments.Length > 0 && ((et.PropertyType).GenericTypeArguments[0].Name == "Boolean"))
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()))
                                                                et.SetValue(detailEntity, Convert.ToBoolean(request.Params.GetValues("item." + et.Name)[i]));
                                                            else
                                                                et.SetValue(detailEntity, false);
                                                        }
                                                        else if(!String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()))
                                                        {
                                                            et.SetValue(detailEntity, request.Params.GetValues("item." + et.Name)[i]);
                                                        }
                                                    }
                                                }
                                                else if (et.Name == "formid")
                                                {
                                                    et.SetValue(detailEntity, fields[0].GetValue(entity, null));
                                                }

                                            }

                                            db.Entry(detailEntity).State = System.Data.Entity.EntityState.Added;
                                            db.SaveChanges();


                                        }
                                    }
                                }
                            }
                        }

                    }

                    transaction.Complete();
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
               //return null;
            }
        }

        public static T UpdateData(DbContext db, T entity, HttpRequestBase request)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    db.Set<T>().Attach(entity);
                    db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;

                    int count = db.SaveChanges();
                    if (count > 0)
                    {
                        PropertyInfo[] fields = entity.GetType().GetProperties();
                        foreach (PropertyInfo ety in fields)
                        {
                            if (ety.PropertyType.Namespace == "System.Collections.Generic")
                            {
                                PropertyInfo[] dettailfields = ety.PropertyType.GenericTypeArguments[0].GetProperties();

                                if (request.Params.GetValues("item." + dettailfields[3].Name) != null)
                                {
                                    for (int i = 0; i < request.Params.GetValues("item." + dettailfields[3].Name).Length; i++)
                                    {
                                        if (!String.IsNullOrEmpty(request.Params.GetValues("item." + dettailfields[3].Name)[i]))
                                        {
                                            dynamic detailModel = (dynamic)ety.PropertyType.GenericTypeArguments[0];

                                            var detailEntity = detailModel.Assembly.CreateInstance(detailModel.FullName);

                                            foreach (PropertyInfo et in dettailfields)
                                            {
                                                if (et.Name != "id" && et.Name != "formid")
                                                {
                                                    if (request.Params.GetValues("item." + et.Name) != null && request.Params.GetValues("item." + et.Name).Length > i)
                                                    {
                                                        if ((et.PropertyType).GenericTypeArguments.Length > 0 && (et.PropertyType).GenericTypeArguments[0].Name == "DateTime")
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()))
                                                                et.SetValue(detailEntity, Convert.ToDateTime(request.Params.GetValues("item." + et.Name)[i]));
                                                        }
                                                        else if ((et.PropertyType).GenericTypeArguments.Length > 0 && (et.PropertyType).GenericTypeArguments[0].Name == "Decimal")
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()) && request.Params.GetValues("item." + et.Name)[i].ToString() != "null")
                                                                et.SetValue(detailEntity, Convert.ToDecimal(request.Params.GetValues("item." + et.Name)[i]));
                                                        }
                                                        else if (((et.PropertyType).GenericTypeArguments.Length > 0 && ((et.PropertyType).GenericTypeArguments[0].Name == "Int32")) || et.PropertyType.FullName == "System.Int32")
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()) && request.Params.GetValues("item." + et.Name)[i].ToString() != "null")
                                                                et.SetValue(detailEntity, Convert.ToInt32(request.Params.GetValues("item." + et.Name)[i]));
                                                        }
                                                        else if ((et.PropertyType).GenericTypeArguments.Length > 0 && ((et.PropertyType).GenericTypeArguments[0].Name == "Int64"))
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()) && request.Params.GetValues("item." + et.Name)[i].ToString() != "null")
                                                                et.SetValue(detailEntity, Convert.ToInt64(request.Params.GetValues("item." + et.Name)[i]));
                                                        }
                                                        else if ((et.PropertyType).GenericTypeArguments.Length > 0 && ((et.PropertyType).GenericTypeArguments[0].Name == "Boolean"))
                                                        {
                                                            if (request.Params.GetValues("item." + et.Name)[i] != null && !String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()))
                                                                et.SetValue(detailEntity, Convert.ToBoolean(request.Params.GetValues("item." + et.Name)[i]));
                                                            else
                                                                et.SetValue(detailEntity, false);
                                                        }
                                                        else if(!String.IsNullOrEmpty(request.Params.GetValues("item." + et.Name)[i].ToString()))
                                                        {
                                                            et.SetValue(detailEntity, request.Params.GetValues("item." + et.Name)[i]);
                                                        }
                                                    }
                                                }
                                                else if (et.Name == "formid")
                                                {
                                                    et.SetValue(detailEntity, fields[0].GetValue(entity, null));
                                                }

                                            }

                                            db.Entry(detailEntity).State = System.Data.Entity.EntityState.Added;
                                            db.SaveChanges();
                                             
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        entity = null;
                    }

                    transaction.Complete();
                }

                return entity;

            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
        }
    }
}