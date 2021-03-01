using System;
using System.Data.Odbc;
namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public string GetFinancialEligibility(string facility, string patientId, double episodeNumber)
        {
            string commandString = $@"SELECT billing_guar_table.financial_class_code	
                                        FROM billing_guar_table
                                        LEFT OUTER JOIN billing_guar_subs_data ON billing_guar_table.FACILITY = billing_guar_subs_data.FACILITY
                                        AND billing_guar_table.GUARANTOR_ID = billing_guar_subs_data.GUARANTOR_ID
                                        WHERE billing_guar_subs_data.guarantor_order = 1
                                        AND billing_guar_subs_data.FACILITY=?                                    
                                        AND billing_guar_subs_data.PATID=?
                                        AND billing_guar_subs_data.EPISODE_NUMBER=?";

            try
            {
                string result = GetPatientString(_connectionStringCollection.PM, commandString, facility, patientId, episodeNumber);
                logger.Debug("GetPatientString Result: " + result);
                return result;
                
            }
            catch (OdbcException ex)
            {
                logger.Error(ex, "GetFinancialEligibility: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.PM, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "GetFinancialEligibility: An error occurred. Error: {errorMessage}.", ex.Message);
                throw;
            }
        }
    }
}
