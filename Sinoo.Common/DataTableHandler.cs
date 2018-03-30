using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Sinoo.Common
{
    /// <summary>
    /// Datatable操作类
    /// </summary>
    public class DataTableHandler
    {
        /// <summary>
        /// 将数据表转化成list对象
        /// </summary>
        /// <returns>返回list对象</returns>
        public List<Dictionary<string, object>> GetList(DataTable dt)
        {
            //实例化list表
            List<Dictionary<string, object>> returnlist = new List<Dictionary<string, object>>();
            //第一行添加列名
            Dictionary<string, object> dcname = new Dictionary<string, object>();
            foreach (DataColumn item in dt.Columns)
            {
                dcname.Add(dt.Columns.IndexOf(item).ToString(), item.ColumnName);

            }
            returnlist.Add(dcname);
            //添加数据
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                bool bl = false;//可以添加默认
                foreach (DataColumn dc in dt.Columns)
                {
                    object obj = dr[dc];
                    dic.Add(dc.ColumnName, obj);
                    if (obj != null && !string.IsNullOrEmpty(obj.ToString())) bl = true;
                }
                if (bl)
                    returnlist.Add(dic);
            }

            return returnlist;
        }

        /// <summary>    
        /// DataTable 转换为List 集合    
        /// </summary>    
        /// <typeparam name="TResult">类型</typeparam>    
        /// <param name="dt">DataTable</param>    
        /// <returns></returns>    
        public  List<T> ToList<T>(DataTable dt) where T : class, new()
        {
            //创建一个属性的列表    
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口    

            Type t = typeof(T);

            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表     
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });

            //创建返回的集合    

            List<T> oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例    
                T ob = new T();
                //找到对应的数据  并赋值    
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.    
                oblist.Add(ob);
            }
            return oblist;
        }
    }
}
