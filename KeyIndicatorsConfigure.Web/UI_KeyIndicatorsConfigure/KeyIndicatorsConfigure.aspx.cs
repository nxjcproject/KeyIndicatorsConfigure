using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using KeyIndicatorsConfigure.Service.KeyIndicators;

namespace KeyIndicatorsConfigure.Web.UI_KeyIndicatorsConfigure
{
    public partial class KeyIndicatorsConfigure : WebStyleBaseForEnergy.webStyleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.InitComponts();
            if (!IsPostBack)
            {
#if DEBUG
                ////////////////////调试用,自定义的数据授权
                List<string> m_DataValidIdItems = new List<string>() { "zc_nxjc_byc_byf", "zc_nxjc_qtx_tys" };
                AddDataValidIdGroup("ProductionOrganization", m_DataValidIdItems);
#elif RELEASE
#endif
                string m_PageId = Request.QueryString["PageId"] != null ? Request.QueryString["PageId"] : "";
                //Hiddenfield_PageId.Value = "8D576280-7AE4-419B-9112-95F96E6D87C5";//运行监控
                //Hiddenfield_PageId.Value = "F6EC56E5-90EB-41A1-BFD6-49DE884D07E8";//环境监控
                //Hiddenfield_PageId.Value = "C130108A-3EEF-4FBC-A9DC-301EED2B3616";//全部
                Hiddenfield_PageId.Value = m_PageId;
                this.OrganisationTree_ProductionLine.Organizations = GetDataValidIdGroup("ProductionOrganization");                         //向web用户控件传递数据授权参数
                this.OrganisationTree_ProductionLine.PageName = "KeyIndicatorsConfigure.aspx";   //向web用户控件传递当前调用的页面名称
                this.OrganisationTree_ProductionLine.LeveDepth = 7;
            }
        }
        /// <summary>
        /// 增删改查权限控制
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static char[] AuthorityControl()
        {
            return mPageOpPermission.ToArray();
        }
        [WebMethod]
        public static string GetKeyIndicators(string mOrganizationId, string mPageId)
        {
            DataTable table = KeyIndicatorsConfigureService.GetKeyIndicatorsDataTable(mOrganizationId, mPageId);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;
        }
        [WebMethod]
        public static int AddKeyIndicators(string mOrganizationId, string mItemName, string mUnit, string mValueType, string mCaculateType, string mPageId, string mGroupId, string mTags, string mSubtrahendTags, string mMin, string mMax, string mAlarmH, string mAlarmHH, string mDisplayIndex, string mEnabled, string mMessageEnabled)
        {
            int result = KeyIndicatorsConfigureService.AddKeyIndicatorsConfigure(mOrganizationId, mItemName, mUnit, mValueType, mCaculateType, mPageId, mGroupId, mTags, mSubtrahendTags, mMin, mMax, mAlarmH, mAlarmHH, mDisplayIndex, mEnabled, mMessageEnabled);
            return result;
        }
        [WebMethod]
        public static int EditKeyIndicators(string mItemId, string mOrganizationId, string mItemName, string mUnit, string mValueType, string mCaculateType, string mPageId, string mGroupId, string mTags, string mSubtrahendTags, string mMin, string mMax, string mAlarmH, string mAlarmHH, string mDisplayIndex, string mEnabled, string mMessageEnabled)
        {
            int result = KeyIndicatorsConfigureService.EditKeyIndicatorsConfigure(mItemId, mOrganizationId, mItemName, mUnit, mValueType, mCaculateType, mPageId, mGroupId, mTags, mSubtrahendTags, mMin, mMax, mAlarmH, mAlarmHH, mDisplayIndex, mEnabled, mMessageEnabled);
            return result;
        }
        [WebMethod]
        public static int deleteKeyIndicators(string mItemId)
        {
            int result = KeyIndicatorsConfigureService.deleteKeyIndicatorsConfigure(mItemId);
            return result;
        }
        [WebMethod]
        public static string PageIdType(string pageItemid)
        {
            DataTable table = KeyIndicatorsConfigureService.PageidTypeSelect(pageItemid);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;
        }
        [WebMethod]
        public static string SelectType(string pageItemid)
        {
            DataTable table = KeyIndicatorsConfigureService.TypeSelect(pageItemid);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;
        }
    }
}