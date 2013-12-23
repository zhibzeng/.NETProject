using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using WindowsService.DBOperation;

namespace WindowsService
{
    /// <summary>
    /// Service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //登陆
        [WebMethod(Description = "验证用户；输入参数：用户名(string),密码(string)；返回：真/假")]
        public Boolean validateUser(string user, string pass)
        {
            if ((user == "admin") && (pass == "admin"))
            {
                return true;
            }
            return false;
        }






        //员工模块
        [WebMethod(Description = "查询所有员工信息")]
        public DataSet QueryAllEmployee()
        {
            string sqlString = "select y.员工代码,y.姓名,y.性别,y.出生日期,f.分公司名称 as 所属分公司,y.照片 from gx2010.员工档案表 ";
            sqlString += "y,gx2010.分公司信息表 f where y.所属分公司代码=f.分公司代码";
            return new DBOperation.DBOperation().selectRecords(sqlString);

        }

        [WebMethod(Description = "删除员工记录")]
        public int DeleteEmployeeByID(string EmployeeID)
        {
            string sqlString = "delete from gx2010.员工档案表 where 员工代码='" + EmployeeID + "'";
            return new DBOperation.DBOperation().OperateRecord(sqlString);

        }


        [WebMethod(Description = "添加//修改y员工信息")]
        public int OperateEmployee(Object[] param, int i)
        {
           
            int result = -1;
            DBOperation.DBOperation dal = new DBOperation.DBOperation();
            switch (i)
            {
                case 1:
                    result = dal.OperateRecord("insert into gx2010.员工档案表 values('" + param[0] + "','" + param[1] + "','" + param[2] + "','" + param[3] + "','" + param[4] + "','" + param[5] + "')");
                    break;
                case 2:
                    result = dal.OperateRecord("update gx2010.员工档案表 set 姓名='" + param[1] + "',性别='" + param[2] + "',出生日期='" + param[3] + "',所属分公司代码='" + param[4] + "',照片='" + param[5] + "' where 员工代码='" + param[0] + "'");
                    break;

            }
            return result;

        }

        [WebMethod(Description = "生成下一个员工代码")]
        public string CalNextEmployeeCode()
        {
            string staffCode;
            string sqlString = "select 员工代码 from gx2010.员工档案表 order by 员工代码 desc";
            DataSet ds = new DataSet();
            ds = new DBOperation.DBOperation().selectRecords(sqlString);
            staffCode = ds.Tables[0].Rows[0][0].ToString().Trim();
            int codeNO = Convert.ToInt32(staffCode.Substring(2, 4));
            return "YG" + (codeNO + 1).ToString();
        }

        [WebMethod(Description = "查询某个员工信息")]
        public DataSet QueryEmployeeByName(string staffName)
        {
            string sqlString = "select y.员工代码,y.姓名,y.性别,y.出生日期,f.分公司名称 as 所属分公司,y.照片 from gx2010.员工档案表 ";
            sqlString += "y,gx2010.分公司信息表 f where y.所属分公司代码=f.分公司代码 and y.姓名='" + staffName + "'";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }





        // 公司模块
        [WebMethod(Description = "查询分公司信息；输入：无；返回DataSet")]
        public DataSet QueryCompanies()
        {
            return new DBOperation.DBOperation().selectRecords("select * from gx2010.分公司信息表");
        }

        [WebMethod(Description = "添加/删除/修改分公司信息；输入：object数组，int i(i=1表示插入 i=2表示更新 i=3表示删除)无；返回DataSet")]
        public int OperaCompanies(Object[] param, int i)
        {
            int result = -1;
            DBOperation.DBOperation dal = new DBOperation.DBOperation();
            //static int result;//返回操作结果
            switch (i)
            {
                case 1:
                    result = dal.OperateRecord("insert into gx2010.分公司信息表 values('" + param[0] + "','" + param[1] + "','" + param[2] + "')");
                    break;
                case 2:
                    result = dal.OperateRecord("update gx2010.分公司信息表 set 分公司名称='" + param[1] + "',区域='" + param[2] + "' where 分公司代码='" + param[0] + "'");
                    break;
                case 3:
                    result = dal.OperateRecord("delete from gx2010.分公司信息表 where 分公司代码='" + param[0] + "'");
                    break;

            }
            return result;

        }





        //产品模块
        [WebMethod(Description = "查询产品列表")]
        public DataSet QueryGoods()
        {
            string sqlString = "select * from gx2010.产品目录";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }

        [WebMethod(Description = "根据代码查询产品")]
        public DataSet QueryGoodsByCode(string code)
        {
            string sqlString = "select * from gx2010.产品目录 c where c.产品代码='"+code+"'";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }
        [WebMethod(Description = "根据名称查询产品")]
        public DataSet QueryGoodsByName(string name) {
            string sqlString = "select * from gx2010.产品目录 c where c.产品名称='" + name.Trim()+"'";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }

        [WebMethod(Description = "按仓库代码查询产品列表")]
        public DataSet QueryGoodsByWarehouseID(string param)
        {
            string sqlString = "select * from gx2010.产品目录 cp ,gx2010.仓库 ck where cp.仓库代码=ck.仓库代码 and ck.仓库代码='" + param + "'";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }

        [WebMethod(Description = "产品信息浏览")]
        public DataSet QueryAllProductInfo() {
            string sqlString = "select cp.产品代码,cp.产品名称,cp.产品单重,cp.产品尺寸,ck.仓库 from gx2010.产品目录 cp,gx2010.仓库 ck where cp.仓库代码=ck.仓库代码";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }
        [WebMethod(Description = "新增/更新产品目录")]
        public int OperateProduct(Object[] param, int op)
        {
            int result = -1;
            DBOperation.DBOperation dal = new DBOperation.DBOperation();
            switch (op) { 
                case 1:
                    result = dal.OperateRecord("update gx2010.产品目录 set 产品名称='" + param[1] + "',产品单重='"+param[2]+"',产品尺寸='"+param[3]+"' "+" where 产品代码='" + param[0] + "'");
                    break;
                case 2:
                    result = dal.OperateRecord("insert into gx2010.产品目录 values('" + param[0] + "','" + param[1] + "'," +Convert.ToDecimal(param[2].ToString().Trim())+ ",'" + param[3] + "','" + param[4] +"')");
                    break;
            }
            return result;
        }
        [WebMethod(Description = "生成下一个产品代码")]
        public string CalNextProductCode()
        {
            string staffCode;
            string sqlString = "select 产品代码 from gx2010.产品目录 order by 产品代码 desc";
            DataSet ds = new DataSet();
            ds = new DBOperation.DBOperation().selectRecords(sqlString);
            staffCode = ds.Tables[0].Rows[0][0].ToString().Trim();
            int codeNO = Convert.ToInt32(staffCode.Substring(2, 2));
            return "JG" + (codeNO + 1).ToString();
        }
        //库存管理
        [WebMethod(Description = "查询采购订单")]
        public DataSet QueryPurOrder(string state)
        {

            string sqlString = "select cg.订单号,cg.时间,cp.产品名称,cg.数量,cg.单价,cg.状态,yg.姓名 as 操作员工,cg.备注 from gx2010.产品目录 cp,";
            sqlString += "gx2010.采购订单 cg,gx2010.员工档案表 yg where cg.产品代码=cp.产品代码 and cg.员工代码=yg.员工代码";
            if (state != "")
            {
                sqlString += " and cg.状态='" + state + "'order by cg.订单号 desc";
            }
            else
            {
                sqlString += " order by cg.订单号 desc";
            }
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }

        [WebMethod(Description = "查询已审批但未入库的采购订单")]
        public DataSet QueryPurNotInOrder() {

            string sqlString = "select 订单号,产品代码,数量,单价,状态,时间,员工代码,备注,是否入库 from gx2010.采购订单 where 状态='已审' and 是否入库 = '0'";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }

        [WebMethod(Description = "根据订单号查询采购订单")]
        public DataSet QueryPurOrderByID(string orderId)
        {
            string sqlString = "select cg.订单号,cg.时间,cp.产品名称,cg.数量,cg.单价,cg.状态,yg.姓名 as 操作员工,cg.备注 from gx2010.产品目录 cp,";
            sqlString += "gx2010.采购订单 cg,gx2010.员工档案表 yg where cg.产品代码=cp.产品代码 and cg.员工代码=yg.员工代码";
            sqlString += " and cg.订单号='" + orderId + "'order by cg.订单号 desc";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }


        [WebMethod(Description = "新增、修改采购订单操作")]
        public int OperatePurOrder(Object[] param, int op)
        {
            int result = -1;
            DBOperation.DBOperation dal = new DBOperation.DBOperation();
            switch (op)
            {
                case 1:
                    result = dal.OperateRecord("insert into gx2010.采购订单 values('" + param[0] + "','" + param[1] + "','" + param[2] + "','" + param[3] + "','" + param[4] + "','" + param[5] + "','" + param[6] + "','" + param[7] + "')");
                    break;
                case 2:
                    result = dal.OperateRecord("update gx2010.采购订单 set 产品代码='" + param[1] + "',数量='"+param[2]+"',单价='"+param[3]+"',状态='"+param[4]+"',时间='"+param[5]+"',员工代码='"+param[6]+"',备注='"+param[7]+"' "+" where 订单号='" + param[0] + "'");
                    break;
                case 3:
                    result = dal.OperateRecord("update gx2010.采购订单 set 是否入库='"+param[1]+"' where 订单号='"+param[0]+"'");
                    break;
            }
            return result;
        }

        [WebMethod(Description = "查询仓库信息")]
        public DataSet QueryWH()
        {
            string sqlString = "select 仓库代码,仓库,地址,主管人员代码 from gx2010.仓库";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }

        
        [WebMethod(Description = "生成采购订单编号")]
        public string CalNextPurchaseCode()
        {
            string purchaseCode;
            string sqlString = "select 订单号 from gx2010.采购订单 order by 订单号 desc";
            DataSet ds = new DataSet();
            ds = new DBOperation.DBOperation().selectRecords(sqlString);
            purchaseCode = ds.Tables[0].Rows[0][0].ToString().Trim();
            int codeNO = Convert.ToInt32(purchaseCode);
            return (codeNO + 1).ToString();
        }
        
        
        [WebMethod(Description = "生成出入库流水号")]
        public string calNextWarehousingCode() {
            string WarehousingCode;
            string sqlString = "select 出入库流水号 from gx2010.出入库存清单 order by 出入库流水号 desc";
            DataSet ds = new DataSet();
            ds = new DBOperation.DBOperation().selectRecords(sqlString);
            WarehousingCode = ds.Tables[0].Rows[0][0].ToString().Trim();
            int codeNO = Convert.ToInt32(WarehousingCode);
            return (codeNO + 1).ToString(); 
            
        }

        [WebMethod(Description = "新增出入库存清单")]
        public int operateWarehousingRecord(Object[] param, int op) { 
         int result = -1;
            DBOperation.DBOperation dal = new DBOperation.DBOperation();
            switch (op)
            { case 1:
                    result = dal.OperateRecord("insert into gx2010.出入库存清单 values('" + param[0] + "','" + param[1] + "','" + param[2] + "','" + param[3] + "','" + param[4] + "','" + param[5] + "')");
                    break;
           
            }
            return result;
        }

        [WebMethod(Description = "根据条件查询出入库存清单信息")]
        public DataSet QueryWarehousing(Object[] param, int op) {
            string sqlString = "";
            
            switch (op) { 
                case 1:
                    sqlString = "select cp.产品代码,kc.出入库时间,kc.出入库数量,kc.出入库流水号,kc.相关订单号,kc.仓库代码 from gx2010.出入库存清单 kc,gx2010.产品目录 cp where kc.产品代码=cp.产品代码 and kc.产品代码='" + param[0] + "'" + " and kc.仓库代码='" + param[1] + "'";
                    break;
            
            }
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }





        //销售模块
        [WebMethod(Description = "获取所有销售订单")]
        public DataSet QuerySaleOrders(string state)
        {

            string sqlString = "select cp.产品名称,s.产品单价,s.销售量,yg.姓名,s.销售流水号,s.状态 from gx2010.产品销售流水帐 s,";
            sqlString += "gx2010.产品目录 cp,gx2010.员工档案表 yg where s.产品代码=cp.产品代码 and s.员工代码=yg.员工代码";
            if (state != "")
            {
                sqlString += " and s.状态='" + state + "'order by s.销售流水号 desc";
            }
            else
            {
                sqlString += " order by s.销售流水号 desc";
            }
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }

        [WebMethod(Description = "对销售订单的更改")]
        public int OperateSaleOrder(Object[] param, int op) { 
            int result = -1;
            DBOperation.DBOperation dal = new DBOperation.DBOperation();
            switch (op) { 
                case 1:
                    result = dal.OperateRecord("update gx2010.产品销售流水帐 set 状态='" + param[1] + "' where 销售流水号='" + param[0] + "'");
                    break;
                case 2:
                    result = dal.OperateRecord("insert into gx2010.产品销售流水帐 values('" + param[0] + "','" + param[1] + "','" + param[2] + "','" + param[3] + "','" + param[4] + "','" + param[5] + "')");
                    break;
                case 3:
                    result = dal.OperateRecord("update gx2010.产品销售流水帐 set 是否出库='" + param[1] + "' where 销售流水号='" + param[0] + "'");
                    break;
            }
            return result;
        }

        [WebMethod(Description = "生成销售流水号")]
        public string CalNextSaleOrderCode() {
            string WarehousingCode;
            string sqlString = "select 销售流水号 from gx2010.产品销售流水帐 order by 销售流水号 desc";
            DataSet ds = new DataSet();
            ds = new DBOperation.DBOperation().selectRecords(sqlString);
            WarehousingCode = ds.Tables[0].Rows[0][0].ToString().Trim();
            int codeNO = Convert.ToInt32(WarehousingCode);
            return (codeNO + 1).ToString(); 
       }

        [WebMethod(Description = "查询已审核但未发货的销售订单")]
        public DataSet QuerySaleNotOutOrders() {
            string sqlString = "select 销售流水号,产品代码,销售量,产品单价,状态,员工代码,是否出库 from gx2010.产品销售流水帐 where 状态='已审' and 是否出库 = '0'";
            return new DBOperation.DBOperation().selectRecords(sqlString);
        }





        [WebMethod(Description = "新增测试数据")]
        public void GenerateTestData() {

            int number = -1;
            DBOperation.DBOperation dal = new DBOperation.DBOperation();
            for (int i = 1; i < 40000; i++) {
                Random r = new Random();
                int n = r.Next(1, 6);
                number = dal.OperateRecord("update gx2010.出入库存清单 set 仓库代码='CK100"+n+"' where 出入库流水号='"+i+"'");
    
            }
        }

    
    }
}
