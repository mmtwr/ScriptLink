using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public ReferralSource GetReferralSourceInfoBySourceCode(string facility, string sourceCode)
        {
            string commandString = @"SELECT SYSTEM.table_referral_sources.ref_source_add_str1,
                                            SYSTEM.table_referral_sources.ref_source_add_str2,
                                            SYSTEM.table_referral_sources.ref_source_add_city,
                                            SYSTEM.table_referral_sources.ref_source_add_state,
                                            SYSTEM.table_referral_sources.ref_source_add_zip,
                                            SYSTEM.table_referral_sources.ref_source_phone
                                       FROM SYSTEM.table_referral_sources
                                      WHERE table_referral_sources.FACILITY=?
                                        AND table_referral_sources.REF_SOURCE_CODE=?";

            ReferralSource referralSource = new ReferralSource()
            { 
                Code = sourceCode
            };
            using (OdbcConnection connection = new OdbcConnection(_connectionStringCollection.PM))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("SOURCECODE", sourceCode));

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                referralSource.Address1 = GetStringValue(reader, "ref_source_add_str1");
                                referralSource.Address2 = GetStringValue(reader, "ref_source_add_str2");
                                referralSource.City = GetStringValue(reader, "ref_source_add_city");
                                referralSource.State = GetStringValue(reader, "ref_source_add_state");
                                referralSource.PostalCode = GetStringValue(reader, "ref_source_add_zip");
                                referralSource.PhoneNumber = GetStringValue(reader, "ref_source_phone");
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetReferralSourceInfoBySourceCode: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.PM, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetReferralSourceInfoBySourceCode: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            return referralSource;
        }
    }
}
