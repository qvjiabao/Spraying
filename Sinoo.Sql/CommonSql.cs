using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinoo.Sql
{
    /// <summary>
    /// 公共Sql语句存储类
    /// </summary>
    public class CommonSql
    {
        #region  插入操作

        /// <summary>
        /// 插入的sql语句 {0}表名 {1}字段名 {2}值
        /// </summary>
        public const string SqlInsert = "INSERT INTO {0}({1}) VALUES ({2}) ;";



        #endregion

        #region 更新操作

        /// <summary>
        /// 修改的Sql语句 {0}表名 {1}赋值过程 {2}条件
        /// </summary>
        public const string SqlUpdate = "UPDATE {0} SET {1} {2} ";

        #endregion

        #region 删除操作

        /// <summary>
        /// 删除SQl语句 {0}表名 {1}条件
        /// </summary>
        public const string SqlDelete = "DELETE FROM {0} {1} ";

        #endregion

        #region 查找操作

        /// <summary>
        /// 查找数据 {0} 列名 {1} 表名 {2}条件
        /// </summary>
        public const string SqlSelect = "SELECT {0} FROM {1} {2} ";

        #endregion

        #region  分页操作

        /// <summary>
        /// 分页sql语句,说明{0}字段's {1}排序字段{2} 正序还是倒叙 DESC(倒叙) ASC(正序) {3}表名 {4}筛选条件 {5}开始条数 {6}结束条数
        /// </summary>
        public const string PageSel = " SELECT * FROM ( SELECT {0},ROW_NUMBER() over(order by {1} {2}) as number FROM {3} {4} ) as Temp WHERE number BETWEEN {5} AND {6} ";


        /// <summary>
        /// 总记录数{0} 表名 {1} 筛选条件
        /// </summary>
        public const string RecordCount = "SELECT COUNT(1) FROM {0} {1}";


        /// <summary>
        /// 分组{0}字段 {1} 表明 {2} 分组字段 {3}排序字段 {4}正序或是序号 {5}开始位置{6}结束位置
        /// </summary>
        //原MySql public const string GroupSelect = "SELECT {0} FROM {1} GROUP BY {2} ORDER BY {3} {4} LIMIT {5},{6}";
        /// <summary>
        /// 获取分组信息（只可查看被分组的字段） {0}字段 {1}表名 {2}查询条件 {3}分组字段 {4}排序字段 {5}排序方式 {6}开始位置 {7}结束位置 
        /// </summary>
        public const string GroupSelect = "SELECT  {0} FROM (SELECT {0},ROW_NUMBER() OVER(ORDER BY {4} {5}) AS number FROM (SELECT {0} FROM {1} AS ftem {2} GROUP BY ftem.{3}) AS ytemp) AS temp WHERE Temp.number>={6} and Temp.number<={7}";

        /// <summary>
        /// 计算总行数
        /// </summary>
        public const string tempSelect = "SELECT COUNT(1) FROM ({0}) Temp";


        #endregion

    }
}
