using SqlServerDataAdapter;
using KeyIndicatorsConfigure.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace KeyIndicatorsConfigure.Service.KeyIndicators
{
    public class KeyIndicatorsConfigureService
    {
        public static DataTable GetKeyIndicatorsDataTable(string organizationId, string PageId)
        {
            string Pageid = "";
            if (PageId == "EnergyMonitor")
            {
                Pageid = "EnergyMonitor";
            }
            if (PageId == "EnvironmentalMonitor")
            {
                Pageid = "EnvironmentalMonitor";
            }
            if (PageId == "All")
            {
                string Pageid1 = "EnergyMonitor";
                string Pageid2 = "EnvironmentalMonitor";
                string connectionString1 = ConnectionStringFactory.NXJCConnectionString;
                ISqlServerDataFactory dataFactory1 = new SqlServerDataFactory(connectionString1);
                string mySql1 = @"SELECT  [ItemId]
                              ,[ItemName]
                              ,[Unit]
                              ,[ValueType]
                              ,[CaculateType]
                              ,[PageId]
                              ,[GroupId]
                              ,[OrganizationID]
                              ,[Tags]
                              ,[SubtrahendTags]
                              ,[Min]
                              ,[Max]
                              ,[AlarmH]
                              ,[AlarmHH]
                              ,[DisplayIndex]
                              ,[Enabled]
                              ,[MessageEnabled]
                          FROM [dbo].[realtime_KeyIndicatorsMonitorContrast]
                          where [OrganizationID]=@organizationId
                                and [Enabled]=1
                                and ([PageId]=@Pageid1 or [PageId]=@Pageid2)
                           order by [DisplayIndex]";
                SqlParameter[] sqlParameter = {new SqlParameter("@organizationId", organizationId),
                                               new SqlParameter("@PageId1", Pageid1),
                                               new SqlParameter("@PageId2", Pageid2)};
                DataTable table = dataFactory1.Query(mySql1, sqlParameter);
                return table;
            }
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);
            string mySql = @"SELECT  [ItemId]
                              ,[ItemName]
                              ,[Unit]
                              ,[ValueType]
                              ,[CaculateType]
                              ,[PageId]
                              ,[GroupId]
                              ,[OrganizationID]
                              ,[Tags]
                              ,[SubtrahendTags]
                              ,[Min]
                              ,[Max]
                              ,[AlarmH]
                              ,[AlarmHH]
                              ,[DisplayIndex]
                              ,[Enabled]
                              ,[MessageEnabled]
                          FROM [dbo].[realtime_KeyIndicatorsMonitorContrast]
                          where [OrganizationID]=@organizationId
                                and [Enabled]=1
                                and [PageId]=@PageId
                           order by [DisplayIndex]";
            SqlParameter[] sqlParameterr = {new SqlParameter("@organizationId", organizationId),
                                           new SqlParameter("@PageId", Pageid)};
            DataTable table1 = dataFactory.Query(mySql, sqlParameterr);
            return table1;
        }
        public static int AddKeyIndicatorsConfigure(string mOrganizationId, string mItemName, string mUnit, string mValueType, string mCaculateType, string mPageId, string mGroupId, string mTags, string mSubtrahendTags, string mMin, string mMax, string mAlarmH, string mAlarmHH, string mDisplayIndex, string mEnabled, string mMessageEnabled)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory factory = new SqlServerDataFactory(connectionString);
            string mySql = @"INSERT INTO [dbo].[realtime_KeyIndicatorsMonitorContrast]
                                    ([ItemId]
                                    ,[ItemName]
                                    ,[Unit]
                                    ,[ValueType]
                                    ,[CaculateType]
                                    ,[PageId]
                                    ,[GroupId]
                                    ,[OrganizationID]
                                    ,[Tags]
                                    ,[SubtrahendTags]
                                    ,[Min]
                                    ,[Max]
                                    ,[AlarmH]
                                    ,[AlarmHH]
                                    ,[DisplayIndex]
                                    ,[Enabled]
                                    ,[MessageEnabled])
                             VALUES
                                   (@mItemId
                                   ,@mItemName
                                   ,@mUnit
                                   ,@mValueType
                                   ,@mCaculateType
                                   ,@mPageId
                                   ,@mGroupId
                                   ,@mOrganizationId
                                   ,@mTags
                                   ,@mSubtrahendTags
                                   ,@mMin
                                   ,@mMax
                                   ,@mAlarmH
                                   ,@mAlarmHH
                                   ,@mDisplayIndex
                                   ,@mEnabled
                                   ,@mMessageEnabled)";
            SqlParameter[] para = { new SqlParameter("@mItemId",System.Guid.NewGuid().ToString()),
                                    new SqlParameter("@mItemName",mItemName),
                                    new SqlParameter("@mUnit", mUnit),
                                    new SqlParameter("@mValueType", mValueType),
                                    new SqlParameter("@mCaculateType",  mCaculateType),
                                    new SqlParameter("@mPageId", mPageId),
                                    new SqlParameter("@mGroupId",mGroupId),
                                    new SqlParameter("@mOrganizationId",mOrganizationId),
                                    new SqlParameter("@mTags", mTags),
                                    new SqlParameter("@mSubtrahendTags", mSubtrahendTags),
                                    new SqlParameter("@mMin",  mMin),
                                    new SqlParameter("@mMax", mMax),
                                    new SqlParameter("@mAlarmH", mAlarmH),
                                    new SqlParameter("@mAlarmHH", mAlarmHH),
                                    new SqlParameter("@mDisplayIndex",  mDisplayIndex),
                                    new SqlParameter("@mEnabled", mEnabled),
                                    new SqlParameter("@mMessageEnabled",mMessageEnabled)};
            int dt = factory.ExecuteSQL(mySql, para);
            return dt;
        }
        public static int EditKeyIndicatorsConfigure(string mItemId, string mOrganizationId, string mItemName, string mUnit, string mValueType, string mCaculateType, string mPageId, string mGroupId, string mTags, string mSubtrahendTags, string mMin, string mMax, string mAlarmH, string mAlarmHH, string mDisplayIndex, string mEnabled, string mMessageEnabled)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory factory = new SqlServerDataFactory(connectionString);

            string mySql = @"UPDATE [dbo].[realtime_KeyIndicatorsMonitorContrast]
                           SET [ItemName]= @mItemName
                            ,[Unit]= @mUnit
                            ,[ValueType]= @mValueType
                            ,[CaculateType]= @mCaculateType
                            ,[PageId]= @mPageId
                            ,[GroupId]= @mGroupId
                            ,[OrganizationID]= @mOrganizationId
                            ,[Tags]= @mTags
                            ,[SubtrahendTags]= @mSubtrahendTags
                            ,[Min]= @mMin
                            ,[Max]= @mMax
                            ,[AlarmH]= @mAlarmH
                            ,[AlarmHH]= @mAlarmHH
                            ,[DisplayIndex]= @mDisplayIndex
                            ,[Enabled]= @mEnabled
                            ,[MessageEnabled]=@mMessageEnabled
                         WHERE [ItemId] = @mItemId";
            SqlParameter[] para = { new SqlParameter("@mItemId",mItemId),
                                    new SqlParameter("@mItemName",mItemName),
                                    new SqlParameter("@mUnit", mUnit),
                                    new SqlParameter("@mValueType", mValueType),
                                    new SqlParameter("@mCaculateType",  mCaculateType),
                                    new SqlParameter("@mPageId", mPageId),
                                    new SqlParameter("@mGroupId",mGroupId),
                                    new SqlParameter("@mOrganizationId",mOrganizationId),
                                    new SqlParameter("@mTags", mTags),
                                    new SqlParameter("@mSubtrahendTags", mSubtrahendTags),
                                    new SqlParameter("@mMin",  mMin),
                                    new SqlParameter("@mMax", mMax),
                                    new SqlParameter("@mAlarmH", mAlarmH),
                                    new SqlParameter("@mAlarmHH", mAlarmHH),
                                    new SqlParameter("@mDisplayIndex",  mDisplayIndex),
                                    new SqlParameter("@mEnabled", mEnabled),
                                    new SqlParameter("@mMessageEnabled",mMessageEnabled)};
            int dt = factory.ExecuteSQL(mySql, para);
            return dt;
        }
        public static int deleteKeyIndicatorsConfigure(string mItemId) 
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory factory = new SqlServerDataFactory(connectionString);

            string mySql = @"delete from [dbo].[realtime_KeyIndicatorsMonitorContrast]
                         WHERE [ItemId] =@mItemId";
            SqlParameter para = new SqlParameter("@mItemId", mItemId);
            int dt = factory.ExecuteSQL(mySql, para);
            return dt;     
        }
        public static DataTable PageidTypeSelect(string pageItemid)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);

            string mySql = @"select [PAGE_ID] from [IndustryEnergy_SH].[dbo].[content] WHERE [NODE_ID] =@pageItemid";
            SqlParameter para = new SqlParameter("@pageItemid", pageItemid);
            DataTable Table = dataFactory.Query(mySql, para);
            string mPageId = "";
            if (Table.Rows.Count != 0)
            {
                mPageId = Table.Rows[0]["PAGE_ID"].ToString();
            }
            DataTable table = new DataTable();
            if (mPageId == "EnergyMonitor")
            {
                string mysql = @"select [PAGE_ID]='EnergyMonitor',[PageIdName]='运行监控'";
                table = dataFactory.Query(mysql);
            }
            if (mPageId == "EnvironmentalMonitor")
            {
                string mysql = @"select [PAGE_ID]='EnvironmentalMonitor',[PageIdName]='环境监控'";
                table = dataFactory.Query(mysql);
            }
            if (mPageId == "All")
            {
                string mysql = @"select [PAGE_ID]='All',[PageIdName]='全部'
                                union all
                                  select [PAGE_ID]='EnergyMonitor',[PageIdName]='运行监控'
                                union all
                                 select [PAGE_ID]='EnvironmentalMonitor',[PageIdName]='环境监控'
                                ";
                table = dataFactory.Query(mysql);
            }
            return table;
        }

        public static DataTable TypeSelect(string pageItemid)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            ISqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionString);

            string mySql = @"select [PAGE_ID] from [IndustryEnergy_SH].[dbo].[content] WHERE [NODE_ID] =@pageItemid";
            SqlParameter para = new SqlParameter("@pageItemid", pageItemid);
            DataTable Table = dataFactory.Query(mySql, para);
            string mPageId = "";
            if (Table.Rows.Count != 0)
            {
                mPageId = Table.Rows[0]["PAGE_ID"].ToString();
            }
            DataTable table = new DataTable();
            if (mPageId == "EnergyMonitor")
            {
                string mysql = @"select [PAGE_ID]='EnergyMonitor',[PageIdName]='运行监控'";
                table = dataFactory.Query(mysql);
            }
            if (mPageId == "EnvironmentalMonitor")
            {
                string mysql = @"select [PAGE_ID]='EnvironmentalMonitor',[PageIdName]='环境监控'";
                table = dataFactory.Query(mysql);
            }
            if (mPageId == "All")
            {
                string mysql = @"
                                 select [PAGE_ID]='EnergyMonitor',[PageIdName]='运行监控'
                                union all
                                 select [PAGE_ID]='EnvironmentalMonitor',[PageIdName]='环境监控'
                                ";
                table = dataFactory.Query(mysql);
            }
            return table;
        }
    }
}
