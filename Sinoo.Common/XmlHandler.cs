using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Data;

namespace Sinoo.Common
{
    /// <summary>
    /// XML操作类库 
    /// </summary>
    public class XmlHandler
    {
        /*
            * CreateXML 创建XML文件和添加数据，便于之后的操作
            * AddItem 追加数据
            * ReadText 读取数据
            * UpdateText 更新数据
            * DelNode 删除数据
            * NodeCount 数据条数
            */

        #region 构造函数
        
        /// <summary>
        /// XMl不带参数的构造函数
        /// </summary>
        public XmlHandler(){ }

        /// <summary>
        /// Xml带参数的构造函数
        /// </summary>
        /// <param name="XmlName">Xml名称</param>
        public XmlHandler(string XmlName) 
        {
            _Name = XmlName;
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + _Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
           
        }

        #endregion

        #region 私有变量和公共属性

        /// <summary>
        /// xml名字
        /// </summary>
        private string _Name = "Default.xml";
        /// <summary>
        /// xml名字
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 引用Xml类
        /// </summary>
       public XmlDocument XmlDoc = new XmlDocument();

        /// <summary>
        /// 获取根节点的
        /// </summary>
        /// <param name="AttrName">属性名称</param>
        /// <returns>返回值</returns>
       public string GetRootNodeAttr(string AttrName, string NodeName = "Root")
       {
           try
           {   
               XmlNode xmlnode = XmlDoc.SelectSingleNode(NodeName);
               return xmlnode.Attributes[AttrName].Value;
           }
           catch (Exception)
           {
               //throw ex;
               return null;  //Modified by sang at 2013.09.24 : 出错时返回null值.
           }
       }

        #endregion

        #region 创建Xml

        /// <summary>
        /// 创建Xml
        /// </summary>
        /// <returns>成功返回True失败返回False</returns>
        public bool CreateXml(string RootNode = "Root")
        {
            //设置Xml
            XmlWriterSettings settings = new XmlWriterSettings();
            //设置格式（缩进元素）
            settings.Indent = true;
            settings.IndentChars = "Async";
            settings.Encoding = System.Text.Encoding.UTF8;
            //路径问题
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
            if (!System.IO.Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig"))//如果文件夹不存在
            {
                System.IO.Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig"); //创建文件夹
            }
            if (!File.Exists(path)) //如果文件不存在
            {
                using (XmlWriter Writer = XmlWriter.Create(path, settings))
                {
                    //写入standalone属性
                    XmlDeclaration dec = XmlDoc.CreateXmlDeclaration("1.0", "GB2312", null);
                    Writer.WriteStartDocument(true);


                    Writer.WriteStartElement(RootNode);
                    //Writer.WriteComment(""); 写入备注
                    //写入属性和属性名字
                    Writer.WriteStartAttribute("SpaceType", "Excel配置文件");
                    Writer.WriteStartAttribute("PinZhi", "优");
                    //写入创建人信息
                    Writer.WriteCData("创建时间：" + DateTime.Now.ToString() + "；创建者：异步科技。 文件编码：" + Guid.NewGuid().ToString());
                    //添加注释
                    Writer.WriteComment("作者：Async");
                    //关闭跟元素，并且写结束标签
                    Writer.WriteEndElement();
                    //将xml关闭并保存
                    Writer.Flush();
                    Writer.Close();

                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 从XML读取数据

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllData(string XmlName = "Root")
        {
            try
            {
                DataTable result = new DataTable();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);
                    XmlNode Root = XmlDoc.SelectSingleNode(XmlName);
                    XmlNodeList XmlList = Root.ChildNodes;
                    //添加数据列
                    foreach (XmlNode Node in XmlList)
                    {
                        if (result.Columns.Count <= 0)   //添加数据列
                        {
                            if (Node.Attributes != null)
                            {
                                foreach (XmlAttribute item in Node.Attributes)
                                {
                                    DataColumn dc = new DataColumn(item.Name);
                                    dc.DataType = typeof(String);
                                    result.Columns.Add(dc);
                                }
                                //添加文本字段
                                DataColumn DcText = new DataColumn();
                                DcText.ColumnName = "InnerText";
                                DcText.DataType = typeof(String);
                                result.Columns.Add(DcText);
                            }

                        }

                        //添加数据部分
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            DataRow dr = result.NewRow();
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                dr[item.Name] = item.Value;
                            }
                            dr["InnerText"] = XmlEle.InnerText;
                            result.Rows.Add(dr);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("转换成数据表的时候");
            }
        }

        /// <summary>
        /// 获取需要的数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetWhereData(string Attr, string obj, string XmlName = "Root")
        {
            try
            {
                DataTable result = new DataTable();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);
                    XmlNode Root = XmlDoc.SelectSingleNode(XmlName);
                    XmlNodeList XmlList = Root.ChildNodes;
                    //添加数据列
                    foreach (XmlNode Node in XmlList)
                    {
                        if (result.Columns.Count <= 0)   //添加数据列
                        {
                            if (Node.Attributes == null) continue;
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                DataColumn dc = new DataColumn(item.Name);
                                dc.DataType = typeof(String);
                                result.Columns.Add(dc);
                            }
                            //添加文本字段
                            DataColumn DcText = new DataColumn();
                            DcText.ColumnName = "InnerText";
                            DcText.DataType = typeof(String);
                            result.Columns.Add(DcText);
                        }

                        //添加数据部分
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            //加判断是否有满足条件
                            if (XmlEle.GetAttribute(Attr) != obj) continue;
                            DataRow dr = result.NewRow();
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                dr[item.Name] = item.Value;
                            }
                            dr["InnerText"] = XmlEle.InnerText;
                            result.Rows.Add(dr);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("转换成数据表的时候");
            }
        }

        /// <summary>
        /// 返回递归数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecursionData(string sonAttr, string ParAttr, string Obj, string XmlName = "Root")
        {
            try
            {
                DataTable result = new DataTable();
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);
                    XmlNode Root = XmlDoc.SelectSingleNode(XmlName);
                    XmlNodeList XmlList = Root.ChildNodes;
                    //添加数据列
                    foreach (XmlNode Node in XmlList)
                    {
                        if (result.Columns.Count <= 0)   //添加数据列
                        {
                            if (Node.Attributes == null) continue;
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                DataColumn dc = new DataColumn(item.Name);
                                dc.DataType = typeof(String);
                                result.Columns.Add(dc);
                            }
                            //添加文本字段
                            DataColumn DcText = new DataColumn();
                            DcText.ColumnName = "InnerText";
                            DcText.DataType = typeof(String);
                            result.Columns.Add(DcText);
                        }

                        //添加数据部分
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            //加判断是否有满足条件
                            if (XmlEle.GetAttribute(ParAttr) != Obj) continue;
                            DataRow dr = result.NewRow();
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                dr[item.Name] = item.Value;
                            }
                            dr["InnerText"] = XmlEle.InnerText;
                            result.Rows.Add(dr);

                            //递归拿取数据
                            RecursionFunction(result, XmlList, ParAttr, sonAttr, XmlEle.GetAttribute(sonAttr));

                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("转换成数据表的时候");
            }
        }


        private void RecursionFunction(DataTable result, XmlNodeList XmlList, string ParAttr, string sonAttr, string Obj)
        {
            try
            {
                //添加数据列
                foreach (XmlNode Node in XmlList)
                {
                    if (Node.Attributes == null) continue;
                    bool b = false;//默认属性里面不存在数据
                    foreach (XmlAttribute item in Node.Attributes) //判断数据是否存在
                    {
                        if (item.Name == ParAttr && item.Value == Obj)//数据有值
                        { b = true; break; }
                    }
                    if (b)//如果节点
                    {
                        //添加数据部分
                        XmlElement XmlEle = (Node as XmlElement);
                        if (XmlEle != null)
                        {
                            //加判断是否有满足条件
                            if (XmlEle.GetAttribute(ParAttr) != Obj) continue;
                            DataRow dr = result.NewRow();
                            foreach (XmlAttribute item in Node.Attributes)
                            {
                                dr[item.Name] = item.Value;
                            }
                            dr["InnerText"] = XmlEle.InnerText;
                            result.Rows.Add(dr);

                            //递归拿取数据
                            RecursionFunction(result, XmlList, ParAttr, sonAttr, XmlEle.GetAttribute(sonAttr));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 计算某节点下面的子节点的数量

        public int NodeCount(string NodeName = "Root")
        {
            //路径问题
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
            if (File.Exists(path))
            {
                XmlDoc.Load(path);
                XmlNode Root;

                //查找节点
                Root = SelectSingleNode(NodeName);
                XmlNodeList XmlList = Root.ChildNodes;
                //计算行数
                int count = 0;
                //循环
                foreach (XmlNode Node in XmlList)
                {
                    XmlElement XmlEle = (Node as XmlElement);
                    if (XmlEle != null)
                    {
                        count += 1;
                    }
                }
                return count;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region 添加操作

        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="Value">节点的值</param>
        /// <returns>返回XmlNode</returns>
        public XmlNode CreateNode(string NodeName, object Value)
        {
            try
            {
                //创建元素
                XmlElement XmlEle = XmlDoc.CreateElement(NodeName);
                //写入数值
                if (Value != null)
                {
                    XmlEle.InnerText = Value.ToString().Trim();
                }
                return (XmlNode)XmlEle;
            }
            catch (Exception ex)
            {
                throw new Exception("创建XmlNode节点失败");
            }
        }

        /// <summary>
        /// 创建Xml元素
        /// </summary>
        /// <param name="NodeName">元素名称</param>
        /// <param name="Value">元素的值</param>
        /// <returns>返回当前元素</returns>
        public XmlElement CreateElement(string NodeName, object Value)
        {
            try
            {
                //创建元素
                XmlElement XmlEle = XmlDoc.CreateElement(NodeName);
                //写入数值
                if (Value != null)
                {
                    XmlEle.InnerText = Value.ToString().Trim();
                }
                return XmlEle;
            }
            catch (Exception ex)
            {
                throw new Exception("创建XmlNode节点失败");
            }
        }
        
        /// <summary>
        /// 给某一个节点添加子节点
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="xmlnode">添加的节点</param>
        /// <returns>成功返回True</returns>
        public XmlNode AddSingleNode(string NodeName, XmlNode xmlnode)
        {
            try
            {
                //路径问题
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);//加载对象
                    XmlNode Root;
                    //查找节点
                    Root =SelectSingleNode(NodeName);
                    XmlNode mynode = Root.AppendChild(xmlnode);
                    XmlDoc.Save(path);
                    return mynode;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("添加节点失败！");
            }
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="xmlnode">XmlNode对象</param>
        /// <returns>返回ListNode对象</returns>
        public XmlNodeList AddListNode(string NodeName, XmlNode xmlnode)
        {
            try
            {
                //路径问题
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);//加载对象
                     XmlNodeList RootList;

                    //查找节点
                     RootList = SelectListNode(NodeName);
                     foreach (XmlNode item in RootList)
                     {
                         XmlNode clonenode = xmlnode.Clone();
                         item.AppendChild(clonenode);
                         
                     }
                     XmlDoc.Save(path);
                     return RootList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("添加节点失败！");
            }
        }

        #endregion

        #region 修改操作

        /// <summary>
        /// 修改节点的值
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="Value">值</param>
        /// <returns>返回节点</returns>
        public XmlNode modSingleNode(string NodeName, object Value)
        {
            try
            {
                //路径问题
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);//加载对象
                    XmlNode Root;
                    //查找节点
                    Root = SelectSingleNode(NodeName);
                    if (Value != null)
                        Root.InnerText = Value.ToString().Trim();
                    XmlDoc.Save(path);
                    return Root;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("修改节点失败！");
            }
        }

        /// <summary>
        /// 修改节点的值
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="Value">值</param>
        /// <returns>返回节点List</returns>
        public XmlNodeList ModListNode(string NodeName, object Value)
        {
            try
            {
                //路径问题
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);//加载对象
                    XmlNodeList RootList;
                    //查找节点
                    RootList = SelectListNode(NodeName);
                    foreach (XmlNode item in RootList)
                    {
                        item.InnerText = Value.ToString().Trim();
                    }
                    XmlDoc.Save(path);
                    return RootList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("添加节点失败！");
            }
        }


        #endregion

        #region 删除节点

        /// <summary>
        /// 删除节点的值
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="Value">值</param>
        /// <returns>返回节点</returns>
        public bool RemSingleNode(string NodeName)
        {
            try
            {
                //路径问题
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);//加载对象
                    XmlNode Root;
                    //查找节点
                    Root = SelectSingleNode(NodeName);
                    if (!Root.IsReadOnly)
                        Root.RemoveAll();

                    Root.ParentNode.RemoveChild(Root);

                    XmlDoc.Save(path);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("修改节点失败！");
            }
        }

        /// <summary>
        /// 删除节点的值
        /// </summary>
        /// <param name="NodeName">节点名称</param>
        /// <param name="Value">值</param>
        /// <returns>返回节点List</returns>
        public bool RemListNode(string NodeName)
        {
            try
            {
                //路径问题
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);//加载对象
                    XmlNodeList RootList;
                    //查找节点
                    RootList = SelectListNode(NodeName);
                    foreach (XmlNode item in RootList)
                    {
                        if (!item.IsReadOnly)
                            item.RemoveAll();
                        item.ParentNode.RemoveChild(item);
                    }
                    
                    XmlDoc.Save(path);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("添加节点失败！");
            }
        }

        #endregion

        #region 递归查找节点

        private XmlNode SelectSingleNode(string NodeName)
        {
            try
            {
                //路径问题
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);//加载对象｝
                    XmlNode xmlnode = null;
                    XmlNodeList NodeList = XmlDoc.ChildNodes;
                    foreach (XmlNode item in NodeList)
                    {
                        if (item.Name == NodeName)
                        {
                            xmlnode = item; break;
                        }
                        SelectSingleNodedg(NodeName, item,ref xmlnode);
                    }

                    return xmlnode;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 内部递归
        /// </summary>
        /// <param name="NodeName"></param>
        /// <param name="xmlnode"></param>
        private void SelectSingleNodedg(string NodeName,XmlNode xmlnode,ref XmlNode Value)
        {
            if (xmlnode.HasChildNodes)
            {
                foreach (XmlNode item in xmlnode.ChildNodes)
                {
                    if (item.Name == NodeName)
                    {
                        Value = item; break;
                    }
                    SelectSingleNodedg(NodeName, item,ref Value);
                }
            }
        }

        /// <summary>
        /// ListNode
        /// </summary>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        private XmlNodeList SelectListNode(string NodeName)
        {
            try
            {
                //路径问题
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "ExcelConfig\\" + Name;
                if (File.Exists(path))
                {
                    XmlDoc.Load(path);//加载对象｝
                    XmlNodeList xmlnodeList = XmlDoc.SelectNodes(NodeName);
                    if (xmlnodeList != null && xmlnodeList.Count>0) return xmlnodeList;

                    //接着进行查找
                    XmlNodeList NodeList = XmlDoc.ChildNodes;
                    foreach (XmlNode item in NodeList)
                    {
                        XmlNodeList listnode=item.SelectNodes(NodeName);
                        if (listnode != null && listnode.Count > 0)
                        {
                            xmlnodeList = listnode; break;
                        }
                        SelectListNodedg(NodeName, item, ref xmlnodeList);
                    }

                    return xmlnodeList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ListNode递归
        /// </summary>
        /// <param name="NodeName"></param>
        /// <param name="xmlnode"></param>
        /// <param name="Value"></param>
        public void SelectListNodedg(string NodeName,XmlNode xmlnode,ref XmlNodeList Value)
        {
            if (xmlnode.HasChildNodes)
            {
                foreach (XmlNode item in xmlnode.ChildNodes)
                {
                    XmlNodeList listnode=item.SelectNodes(NodeName);
                    if (listnode != null && listnode.Count > 0)
                    {
                        Value = listnode; break;
                    }
                    SelectListNodedg(NodeName, item, ref Value);
                }
            }
        }


        #endregion

    }
}