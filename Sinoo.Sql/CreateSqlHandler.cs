using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Sinoo.Sql
{
    /// <summary>
    /// 创建公共Sql语句
    /// </summary>
    /// <typeparam name="T">泛型类</typeparam>
    public class CreateSqlHandler<T> where T : new()
    {
        #region 公共操作

        /// <summary>
        /// 是否属于基类
        /// </summary>
        /// <param name="Infos">公共属性集</param>
        /// <param name="info">单个公共属性</param>
        /// <returns>在基类里面返回true否则返回false</returns>
        internal bool _IsBase(Type Type, PropertyInfo Info)
        {
            bool b = false;
            foreach (PropertyInfo item in Type.BaseType.GetProperties())
            {
                if (item.Name == Info.Name)
                {
                    b = true;
                }
            }
            return b;
        }

        /// <summary>
        /// 生成字段
        /// </summary>
        /// <returns>返回字段</returns>
        internal StringBuilder _GenerateAllFields()
        {
            T _Model = new T();//实例化实体
            Type _Type = _Model.GetType();//反射返回类型
            StringBuilder fields = new StringBuilder(); //字段容器
            foreach (PropertyInfo Info in _Type.GetProperties()) //循环字段
            {
                if (_IsBase(_Type, Info)) continue;
                fields.Append(Info.Name);
                fields.Append(",");
            }
            if (fields.Length > 0) fields.Remove(fields.Length - 1, 1); //移除最后一个","号
            else fields.Append(" * ");//添加所有
            return fields;
        }

        /// <summary>
        /// 获取有数据的字段
        /// </summary>
        /// <returns>返回字段</returns>
        internal StringBuilder _GenerateDataFields(T _Model)
        {
            Type _Type = _Model.GetType();//反射返回类型
            StringBuilder fields = new StringBuilder(); //字段容器
            foreach (PropertyInfo Info in _Type.GetProperties()) //循环字段
            {
                object obj = Info.GetValue(_Model, null);
                if (obj == null) continue;
                if (_IsBase(_Type, Info)) continue;
                fields.Append(Info.Name);
                fields.Append(",");
            }
            if (fields.Length > 0) fields.Remove(fields.Length - 1, 1); //移除最后一个","号
            else fields.Append(" * ");//添加所有
            return fields;
        }

        /// <summary>
        /// 生成条件
        /// </summary>
        /// <param name="_Model">实体</param>
        /// <returns>返回生成条件</returns>
        internal StringBuilder _GenerateWhere(T _Model)
        {
            //生成空条件
            if (_Model == null) return new StringBuilder("");
            //定义条件
            StringBuilder sb = new StringBuilder(" WHERE ");
            Type _Type = _Model.GetType();
            foreach (PropertyInfo item in _Type.GetProperties())
            {
                object obj = item.GetValue(_Model, null);
                if (obj == null) continue;
                if (!_IsBase(_Type, item)) //如果不是基类的字段
                {
                    if (sb.Length > 7)
                        sb.Append("AND ");
                    sb.Append(item.Name);
                    sb.Append(" =@");
                    sb.Append(item.Name);
                    sb.Append(" ");
                }
            }
            if (sb.Length <= 7) sb.Remove(0, sb.Length); //如果没有条件的话移除条件
            return sb;
        }

        /// <summary>
        /// 生成条件
        /// </summary>
        /// <param name="_Model">实体</param>
        /// <param name="_PageIndex">当前页面</param>
        /// <param name="_PageSzie">页面大小</param>
        /// <returns>返回生成条件</returns>
        internal StringBuilder _GenerateWhere(T _Model, ref int _PageIndex, ref int _PageSzie)
        {
            //生成空条件
            if (_Model == null) return new StringBuilder("");
            //定义条件
            StringBuilder sb = new StringBuilder(" WHERE ");
            Type _Type = _Model.GetType();
            foreach (PropertyInfo item in _Type.GetProperties())
            {
                object obj = item.GetValue(_Model, null);
                if (obj == null) continue;
                if (!_IsBase(_Type, item)) //如果不是基类的字段
                {
                    if (sb.Length > 7)
                        sb.Append("AND ");
                    sb.Append(item.Name);
                    sb.Append(" =@");
                    sb.Append(item.Name);
                    sb.Append(" ");
                }
                else
                {
                    if (item.Name == "PageIndex")
                    {
                        _PageIndex = Convert.ToInt32(obj);
                    }
                    else if (item.Name == "PageSize")
                    {
                        _PageSzie = Convert.ToInt32(obj);
                    }
                }
            }
            if (sb.Length <= 7) sb.Remove(0, sb.Length); //如果没有条件的话移除条件
            return sb;
        }

        /// <summary>
        /// 创建分组信息条件
        /// </summary>
        /// <param name="_Model">实体</param>
        /// <param name="_GroupField">分组字段</param>
        /// <param name="_PageIndex">当前页面</param>
        /// <param name="_PageSzie"></param>
        /// <returns></returns>
        private StringBuilder _GenerateGroupWhere(T _Model, string _GroupField, ref int _PageIndex, ref int _PageSzie)
        {
            if (_Model == null) return new StringBuilder("");
            //定义条件
            StringBuilder sb = new StringBuilder(" WHERE ");
            Type _Type = _Model.GetType();
            foreach (PropertyInfo item in _Type.GetProperties())
            {
                object obj = item.GetValue(_Model, null);
                if (obj == null) continue;
                if (!_IsBase(_Type, item)) //如果不是基类的字段
                {
                    if (sb.Length > 7)
                        sb.Append("AND ");
                    sb.Append(item.Name);
                    sb.Append(" =@");
                    sb.Append(item.Name);
                    sb.Append(" ");
                }
                else
                {
                    if (item.Name == "PageIndex")
                    {
                        _PageIndex = Convert.ToInt32(obj);
                    }
                    else if (item.Name == "PageSize")
                    {
                        _PageSzie = Convert.ToInt32(obj);
                    }
                }
            }
            if (sb.Length <= 7) sb.Remove(0, sb.Length); //如果没有条件的话移除条件

            if (_GroupField != String.Empty && _GroupField != null)
            {
                sb.Append(" ");
                sb.Append(" GROUP BY ");
                sb.Append(_GroupField);
            }
            return sb;
        }


        #endregion

        #region 添加数据库

        /// <summary>
        /// 生成添加字段和数据参数
        /// </summary>
        /// <returns>返回字符串</returns>
        public List<StringBuilder> ValueFieldsAndParam(T Model)
        {
            List<StringBuilder> lsb = new List<StringBuilder>();
            StringBuilder sbfield = new StringBuilder("");//字段
            StringBuilder sbparam = new StringBuilder("");//参数

            Type _Type = Model.GetType();//反射返回类型
            foreach (PropertyInfo Info in _Type.GetProperties())
            {
                object obj = Info.GetValue(Model, null);//获取值
                if (obj == null) continue;
                sbfield.Append(Info.Name);
                sbfield.Append(",");
                sbparam.Append("'");
                sbparam.Append(obj.ToString());
                sbparam.Append("',");
            }
            if (sbfield.Length > 0) sbfield.Remove(sbfield.Length - 1, 1);
            if (sbparam.Length > 0) sbparam.Remove(sbparam.Length - 1, 1);
            lsb.Add(sbfield);//添加字段
            lsb.Add(sbparam);//添加参数
            return lsb;
        }

        /// <summary>
        /// 生成添加字符串
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="Model">Table名字</param>
        /// <returns>返回添加的字符串</returns>
        public string Insert(T Model, string TableName)
        {
            List<StringBuilder> lsb = ValueFieldsAndParam(Model);
            return String.Format(CommonSql.SqlInsert, TableName, lsb[0].ToString(), lsb[1].ToString());
        }


        #endregion

        #region  修改数据库

        /// <summary>
        /// 返回更新的sql语句
        /// </summary>
        /// <param name="Model">实体</param>
        /// <returns>添加</returns>
        public StringBuilder UpDateSql(T _Model)
        {
            StringBuilder sb = new StringBuilder();
            Type _Type = _Model.GetType();//反射返回类型
            foreach (PropertyInfo Info in _Type.GetProperties())
            {
                object obj = Info.GetValue(_Model, null);//获取值
                if (obj == null) continue;
                if (Info.Name.EndsWith("001"))continue;
                sb.Append(Info.Name);
                sb.Append("=");
                sb.Append("'");
                sb.Append(obj.ToString());
                sb.Append("',");
            }
            if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
            return sb;
        }

        /// <summary>
        /// 更新sql语句
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="TableName">表名</param>
        /// <param name="Where">条件</param>
        /// <returns>返回sql语句</returns>
        public string Update(T Model,string TableName, string Where)
        {
            return String.Format(CommonSql.SqlUpdate, TableName, UpDateSql(Model), Where);
        }



        #endregion

        #region 删除数据库

        /// <summary>
        /// 删除数据通过条件
        /// </summary>
        /// <param name="_Model">实体</param>
        /// <param name="_Where"></param>
        /// <returns>返回删除sql语句</returns>
        public string Delete(T _Model, string _Where)
        {
            return String.Format(CommonSql.SqlDelete, _Model.GetType().Name, _Where);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="_Model">实体</param>
        /// <returns>返回删除sql语句</returns>
        public string Delete(T _Model)
        {
            StringBuilder Where = _GenerateWhere(_Model);
            return String.Format(CommonSql.SqlDelete, _Model.GetType().Name, Where);
        }

        #endregion

        #region  查找数据库

        /// <summary>
        /// 查询数据表
        /// </summary>
        /// <param name="_Model">Model实体</param>
        /// <returns>返回 删除的字符串</returns>
        public string Select(T _Model)
        {
            StringBuilder Where = _GenerateWhere(_Model);
            return String.Format(CommonSql.SqlSelect, _GenerateAllFields().ToString(), _Model.GetType().Name, Where);
        }

        /// <summary>
        /// 查询数据表
        /// </summary>
        /// <param name="_Model">Model实体</param>
        /// <param name="_Where">条件</param>
        /// <returns>返回 删除的字符串</returns>
        public string Select(T _Model, string _Where)
        {
            string result = String.Format(CommonSql.SqlSelect, _GenerateAllFields(), _Model.GetType().Name, _Where);
            return result;
        }

        #endregion

        #region 分页功能

        /// <summary>
        /// 查询数据By分页
        /// </summary>
        /// <param name="_Model">Model实体</param>
        /// <returns>返回字符串</returns>
        public string SelectByPage(T _Model, string SortField, string Sort)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = _GenerateWhere(_Model, ref PageIndex, ref PageSzie);
            return String.Format(CommonSql.PageSel, _GenerateAllFields().ToString(), SortField, Sort, _Model.GetType().Name, Where, ((PageIndex - 1) * PageSzie + 1).ToString(), (PageIndex * PageSzie).ToString());
        }
        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="_Model">实体</param>
        /// <returns>返回总页数</returns>
        public string SelectByPage(T _Model)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = _GenerateWhere(_Model, ref PageIndex, ref PageSzie);
            return String.Format(CommonSql.RecordCount, _Model.GetType().Name, Where);
        }

        /// <summary>
        /// 查询数据By分页
        /// </summary>
        /// <param name="Model">Model实体</param>
        /// <returns>返回字符串</returns>
        public string SelectByPage(T Model, string SortField, string Sort, string Where)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            _GenerateWhere(Model, ref PageIndex, ref PageSzie);
            return String.Format(CommonSql.PageSel, _GenerateAllFields().ToString(), SortField, Sort, Model.GetType().Name, Where, ((PageIndex - 1) * PageSzie + 1).ToString(), (PageIndex * PageSzie).ToString());
        }
        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="Model"></param>
        /// <returns>返回总页数</returns>
        public string SelectByPage(T Model, string Where)
        {
            return String.Format(CommonSql.RecordCount, Model.GetType().Name, Where);
        }


        /// <summary>
        /// 查询数据By分页分组页面
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="GroupField">分组</param>
        /// <param name="SortField">排序字段</param>
        /// <param name="Sort">排序</param>
        /// <returns>分组分页</returns>
        public string SelectByGPage(T Model, string GroupField, string SortField, string Sort)
        {
            int PageIndex = 0; //索引页面
            int PageSzie = 0; //页面大小
            StringBuilder Where = _GenerateGroupWhere(Model, GroupField, ref PageIndex, ref PageSzie);
            return String.Format(CommonSql.GroupSelect, GroupField, Model.GetType().Name, SortField, SortField, Sort, ((PageIndex - 1) * PageSzie + 1).ToString(), (PageIndex * PageSzie).ToString());
        }


        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="Model"></param>
        /// <returns>返回总页数</returns>
        public string SelectByGPage(T Model, string GroupField)
        {
            return String.Format(CommonSql.tempSelect, String.Format(CommonSql.SqlSelect, GroupField, Model.GetType().Name, " GROUP BY  " + GroupField + ""));
        }

        #endregion
    }
}
