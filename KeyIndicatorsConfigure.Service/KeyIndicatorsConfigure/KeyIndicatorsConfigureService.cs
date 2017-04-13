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
        public static DataTable GetKeyIndicatorsDataTable(string organizationId)
        {
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
                          FROM [dbo].[realtime_KeyIndicatorsMonitorContrast]
                          where [OrganizationID]=@organizationId
                                and [Enabled]=1
                           order by [DisplayIndex]";
            SqlParameter sqlParameter = new SqlParameter("@organizationId", organizationId);
            DataTable table = dataFactory.Query(mySql, sqlParameter);
            return table;
        }
        public static int AddKeyIndicatorsConfigure(string mOrganizationId, string mItemName, string mUnit, string mValueType, string mCaculateType, string mPageId, string mGroupId, string mTags, string mSubtrahendTags, string mMin, string mMax, string mAlarmH, string mAlarmHH, string mDisplayIndex, string mEnabled)
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
                                    ,[Enabled])
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
                                   ,@mEnabled)";
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
                                    new SqlParameter("@mEnabled", mEnabled)};
            int dt = factory.ExecuteSQL(mySql, para);
            return dt;
        }
        public static int EditKeyIndicatorsConfigure(string mItemId, string mOrganizationId, string mItemName, string mUnit, string mValueType, string mCaculateType, string mPageId, string mGroupId, string mTags, string mSubtrahendTags, string mMin, string mMax, string mAlarmH, string mAlarmHH, string mDisplayIndex, string mEnabled)
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
                                    new SqlParameter("@mEnabled", mEnabled)};
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
    }
}
