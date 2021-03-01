using Rosecrance.ScriptLinkService.Data.Models;
using System;
using System.Data.Odbc;

namespace Rosecrance.ScriptLinkService.Data.Repositories.Odbc
{
    public partial class GetOdbcDataRepository
    {
        public CombinedAssessment GetClientCombinedAssessmentInfoByPatientId(string facility, string patientId, double episodeNumber = 0)
        {
            
            string episode="";
            string commandString;

            if (facility != "5")
            {
                commandString = $@"SELECT TOP 1 rose_asmnt_pppe.DATA_ENTRY_DATE AS DatePPPE
                                                   , rose_asmnt_pppe.DATA_ENTRY_TIME AS TimePPPE
                                                   , TO_TIMESTAMP(rose_asmnt_pppe.DATA_ENTRY_TIME,'HH:MI:SS.FF') AS EntryTime
                                                   , rose_asmnt_single_form.dt_start AS RCAStartDate
                                                   , rose_asmnt_single_form.DATA_ENTRY_DATE AS DateSingleForm
                                                   , rose_asmnt_single_form.DATA_ENTRY_TIME AS TimeSingleForm
                                                   -- ASMNT PPPE
                                                   , rose_asmnt_pppe.FACILITY
                                                   , rose_asmnt_pppe.PATID
                                                   , rose_asmnt_pppe.EPISODE_NUMBER
                                                   , rose_asmnt_pppe.pppe_problem
                                                   , rose_asmnt_pppe.pppe_duration
                                                   , rose_asmnt_pppe.pppe_severity
                                                   , rose_asmnt_pppe.pppe_othersx_co
                                                   , rose_asmnt_pppe.pppe_sa
                                                   , rose_asmnt_pppe.pppe_mh
                                                   -- ASMNT MH STATUS
                                                   , rose_asmnt_mh_status.mhs_fam_co
                                                   -- ASMNT DRUGS OF CHOICE
                                                   , rose_asmnt_drugsofchoice.doc_support
                                                   , rose_asmnt_drugsofchoice.doc_fam_perception
                                                   , rose_asmnt_drugsofchoice.doc_gen_subusehx 
                                                   , rose_asmnt_drugsofchoice.doc_stage_of_change_Value
                                                   , rose_asmnt_drugsofchoice.doc1_doc_Value
                                                   , rose_asmnt_drugsofchoice.doc1_age1use 
                                                   , TO_CHAR(rose_asmnt_drugsofchoice.doc1_dateused, 'MM-DD-YYYY') AS doc1_dateused
                                                   , rose_asmnt_drugsofchoice.ss_doc1_ia_Value 
                                                   , rose_asmnt_drugsofchoice.doc1_freq_Value
                                                   , rose_asmnt_drugsofchoice.doc1_route_Value
                                                   , rose_asmnt_drugsofchoice.doc1_sx_Value
                                                   -- ASMNT WITHDRAWAL
                                                   , rose_asmnt_withdrawal.ss_auditory_Value
                                                   , rose_asmnt_withdrawal.ss_diaphoresis_Value
                                                   , rose_asmnt_withdrawal.ss_diarrhea_Value
                                                   , rose_asmnt_withdrawal.ss_nausVom_Value
                                                   , rose_asmnt_withdrawal.ss_seizure_Value
                                                   , rose_asmnt_withdrawal.ss_tactile_Value
                                                   , rose_asmnt_withdrawal.ss_tremors_Value
                                                   , rose_asmnt_withdrawal.ss_visual_Value
                                                   , rose_asmnt_withdrawal.ms_oriented_Value 
                                                   , rose_asmnt_withdrawal.ms_symptoms_Value
                                                   -- ASMNT TXHX PRIMARY
                                                   , rose_asmnt_txhx_primary.txhxp_eval_yn
                                                   , rose_asmnt_txhx_primary.txhxp_txhx_yn
                                                   , rose_asmnt_txhx_primary.txhxp_txhx_num       
                                                   , rose_asmnt_txhx_primary.txhxp_selfhelp_yn
                                                   , rose_asmnt_txhx_primary.txhxp_selfhelp_no
                                                   -- ASMNT ISMART
                                                   , rose_asmnt_ismart.nu_prior_tx_admit
                                                   , rose_asmnt_ismart.nu_nonTxSA_hosp
                                                   -- ASMNT MHTXHX PRIMARY
                                                   , rose_asmnt_mhtxhx_primary.mhtxhx_prior_eval
                                                   , rose_asmnt_mhtxhx_primary.mhtxhx_prior_tx
                                                   -- ASMNT SINGLE FORM
                                                   , rose_asmnt_single_form.ms_hxHomicidal_Value
                                                   , rose_asmnt_single_form.ms_curHomicidal_Value
                                                   -- ASMNT SA MH SCREEN
                                                   , rose_asmnt_sa_mh_screen.samh_self_harm_hx
                                                   , rose_asmnt_sa_mh_screen.samh_self_harm_risk_Value
                                                   -- ASMNT PSHXSR
                                                   , rose_asmnt_pshxsr.pshxsr_trauma_mr_Value
                                                   -- ASMNT BM SCREEN
                                                   , rose_asmnt_bm_screen.bms_curMeds_yn
                                                   , rose_asmnt_bm_screen.bms_curMeds_co
                                                   , rose_asmnt_bm_screen.bms_dcMeds_yn
                                                   , rose_asmnt_bm_screen.bms_hspt_sr
                                                   , rose_asmnt_bm_screen.bms_proced_yn
                                                   , rose_asmnt_bm_screen.bms_pe_time_sr_Value
                                                   , rose_asmnt_bm_screen.bms_pe_time_nstxt
                                                   , rose_asmnt_bm_screen.bms_appt_sr
                                                   , rose_asmnt_bm_screen.bms_contagion_mr_Value
                                                   -- ASMNT LGL HX
                                                   , rose_asmnt_lgl_hx.yn_legal
                                                   , rose_asmnt_lgl_hx.yn_lglHx
                                                   , rose_asmnt_lgl_hx.yn_courtOrder_Value
                                                   , rose_asmnt_lgl_hx.yn_records_Value
                                            FROM ROSE.rose_asmnt_single_form
                                            LEFT OUTER JOIN ROSE.rose_asmnt_pppe ON rose_asmnt_single_form.FACILITY = rose_asmnt_pppe.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_pppe.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_pppe.EPISODE_NUMBER    
                                                AND rose_asmnt_single_form.CUSTHCDH_UID = rose_asmnt_pppe.CUSTHCDH_UID
                                            LEFT OUTER JOIN ROSE.rose_asmnt_mh_status ON rose_asmnt_single_form.FACILITY = rose_asmnt_mh_status.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_mh_status.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_mh_status.EPISODE_NUMBER
                                                AND rose_asmnt_single_form.CUSTHCET_UID = rose_asmnt_mh_status.CUSTHCET_UID 
                                            LEFT OUTER JOIN ROSE.rose_asmnt_drugsofchoice ON rose_asmnt_single_form.FACILITY = rose_asmnt_drugsofchoice.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_drugsofchoice.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_drugsofchoice.EPISODE_NUMBER
                                                AND rose_asmnt_single_form.CUSTHCCU_UID = rose_asmnt_drugsofchoice.CUSTHCCU_UID
                                            LEFT OUTER JOIN ROSE.rose_asmnt_withdrawal ON rose_asmnt_single_form.FACILITY = rose_asmnt_withdrawal.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_withdrawal.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_withdrawal.EPISODE_NUMBER
                                                AND rose_asmnt_single_form.CUSTHNFM_UID = rose_asmnt_withdrawal.CUSTHNFM_UID
                                            LEFT OUTER JOIN ROSE.rose_asmnt_txhx_primary ON rose_asmnt_single_form.FACILITY = rose_asmnt_txhx_primary.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_txhx_primary.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_txhx_primary.EPISODE_NUMBER
                                                AND rose_asmnt_single_form.CUSTHCCR_UID = rose_asmnt_txhx_primary.CUSTHCCR_UID
                                                AND rose_asmnt_single_form.CUSTHCCS_UID = rose_asmnt_txhx_primary.CUSTHCCS_UID
                                            LEFT OUTER JOIN ROSE.rose_asmnt_ismart ON rose_asmnt_single_form.FACILITY = rose_asmnt_ismart.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_ismart.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_ismart.EPISODE_NUMBER
                                                AND rose_asmnt_single_form.CUSTHNXJ_UID = rose_asmnt_ismart.CUSTHNXJ_UID
                                            LEFT OUTER JOIN ROSE.rose_asmnt_mhtxhx_primary ON rose_asmnt_single_form.FACILITY = rose_asmnt_mhtxhx_primary.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_mhtxhx_primary.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_mhtxhx_primary.EPISODE_NUMBER 
                                                AND rose_asmnt_single_form.CUSTHCCS_UID = rose_asmnt_mhtxhx_primary.CUSTHCCS_UID
                                            LEFT OUTER JOIN ROSE.rose_asmnt_sa_mh_screen ON rose_asmnt_single_form.FACILITY = rose_asmnt_sa_mh_screen.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_sa_mh_screen.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_sa_mh_screen.EPISODE_NUMBER
                                                AND rose_asmnt_single_form.CUSTHCDI_UID = rose_asmnt_sa_mh_screen.CUSTHCDI_UID
                                            LEFT OUTER JOIN ROSE.rose_asmnt_pshxsr ON rose_asmnt_single_form.FACILITY = rose_asmnt_pshxsr.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_pshxsr.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_pshxsr.EPISODE_NUMBER
                                                AND rose_asmnt_single_form.CUSTHCEX_UID = rose_asmnt_pshxsr.CUSTHCEX_UID
                                            LEFT OUTER JOIN ROSE.rose_asmnt_bm_screen ON rose_asmnt_single_form.FACILITY = rose_asmnt_bm_screen.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_bm_screen.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_bm_screen.EPISODE_NUMBER
                                                AND rose_asmnt_single_form.CUSTHCEU_UID = rose_asmnt_bm_screen.CUSTHCEU_UID
                                            LEFT OUTER JOIN ROSE.rose_asmnt_lgl_hx ON rose_asmnt_single_form.FACILITY = rose_asmnt_lgl_hx.FACILITY 
                                                AND rose_asmnt_single_form.PATID = rose_asmnt_lgl_hx.PATID 
                                                AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_lgl_hx.EPISODE_NUMBER
                                                AND rose_asmnt_single_form.CUSTHNFN_UID = rose_asmnt_lgl_hx.CUSTHNFN_UID
                                            WHERE rose_asmnt_single_form.FACILITY =?
                                            AND rose_asmnt_single_form.PATID =?                                            
                                            {episode}
                                            ORDER BY rose_asmnt_single_form.DATA_ENTRY_DATE DESC, TO_TIMESTAMP(rose_asmnt_single_form.DATA_ENTRY_TIME,'HH:MI:SS.FF') DESC 
                                            ";
            }
            else
            {
                commandString = $@"SELECT TOP 1 MAX(rose_asmnt_pppe.FACILITY) AS FACILITY
                                               , MAX(rose_asmnt_pppe.PATID) AS PATID
                                               , MAX(rose_asmnt_pppe.EPISODE_NUMBER) AS EPISODE_NUMBER
                                               , MAX(rose_asmnt_pppe.pppe_problem) AS pppe_problem
                                               , MAX(rose_asmnt_pppe.pppe_duration) AS pppe_duration
                                               , MAX(rose_asmnt_pppe.pppe_severity) AS pppe_severity
                                               , MAX(rose_asmnt_pppe.pppe_othersx_co) AS pppe_othersx_co
                                               , MAX(rose_asmnt_pppe.pppe_sa) AS pppe_sa
                                               , MAX(rose_asmnt_pppe.pppe_mh) AS pppe_mh
                                               -- ASMNT MH STATUS
                                               , MAX(rose_asmnt_mh_status.mhs_fam_co) AS mhs_fam_co
                                               -- ASMNT DRUGS OF CHOICE
                                               , MAX(rose_asmnt_drugsofchoice.doc_support) AS doc_support
                                               , MAX(rose_asmnt_drugsofchoice.doc_fam_perception) AS doc_fam_perception
                                               , MAX(rose_asmnt_drugsofchoice.doc_gen_subusehx) AS doc_gen_subusehx
                                               , MAX(rose_asmnt_drugsofchoice.doc_stage_of_change_Value) AS doc_stage_of_change_Value
                                               , MAX(rose_asmnt_drugsofchoice.doc1_doc_Value) AS doc1_doc_Value
                                               , MAX(rose_asmnt_drugsofchoice.doc1_age1use) AS doc1_age1use
                                               , MAX(TO_CHAR(rose_asmnt_drugsofchoice.doc1_dateused, 'MM-DD-YYYY')) AS doc1_dateused
                                               , MAX(rose_asmnt_drugsofchoice.ss_doc1_ia_Value) AS ss_doc1_ia_Value
                                               , MAX(rose_asmnt_drugsofchoice.doc1_freq_Value) AS doc1_freq_Value
                                               , MAX(rose_asmnt_drugsofchoice.doc1_route_Value) AS doc1_route_Value
                                               , MAX(rose_asmnt_drugsofchoice.doc1_sx_Value) AS doc1_sx_Value
                                               -- ASMNT WITHDRAWAL
                                               , MAX(rose_asmnt_withdrawal.ss_auditory_Value) AS ss_auditory_Value
                                               , MAX(rose_asmnt_withdrawal.ss_diaphoresis_Value) AS ss_diaphoresis_Value
                                               , MAX(rose_asmnt_withdrawal.ss_diarrhea_Value) AS ss_diarrhea_Value
                                               , MAX(rose_asmnt_withdrawal.ss_nausVom_Value) AS ss_nausVom_Value
                                               , MAX(rose_asmnt_withdrawal.ss_seizure_Value) AS ss_seizure_Value
                                               , MAX(rose_asmnt_withdrawal.ss_tactile_Value) AS ss_tactile_Value
                                               , MAX(rose_asmnt_withdrawal.ss_tremors_Value) AS ss_tremors_Value
                                               , MAX(rose_asmnt_withdrawal.ss_visual_Value) AS ss_visual_Value
                                               , MAX(rose_asmnt_withdrawal.ms_oriented_Value) AS ms_oriented_Value
                                               , MAX(rose_asmnt_withdrawal.ms_symptoms_Value) AS ms_symptoms_Value
                                               -- ASMNT TXHX PRIMARY
                                               , MAX(rose_asmnt_txhx_primary.txhxp_eval_yn) AS txhxp_eval_yn
                                               , MAX(rose_asmnt_txhx_primary.txhxp_txhx_yn) AS txhxp_txhx_yn
                                               , MAX(rose_asmnt_txhx_primary.txhxp_txhx_num) AS txhxp_txhx_num
                                               , MAX(rose_asmnt_txhx_primary.txhxp_selfhelp_yn) AS txhxp_selfhelp_yn
                                               , MAX(rose_asmnt_txhx_primary.txhxp_selfhelp_no) AS txhxp_selfhelp_no
                                               -- ASMNT ISMART
                                               , MAX(rose_asmnt_ismart.nu_prior_tx_admit) AS nu_prior_tx_admit
                                               , MAX(rose_asmnt_ismart.nu_nonTxSA_hosp) AS nu_nonTxSA_hosp
                                               -- ASMNT MHTXHX PRIMARY
                                               , MAX(rose_asmnt_mhtxhx_primary.mhtxhx_prior_eval) AS mhtxhx_prior_eval
                                               , MAX(rose_asmnt_mhtxhx_primary.mhtxhx_prior_tx) AS mhtxhx_prior_tx
                                               -- ASMNT SINGLE FORM
                                               , MAX(rose_asmnt_single_form.ms_hxHomicidal_Value) AS ms_hxHomicidal_Value
                                               , MAX(rose_asmnt_single_form.ms_curHomicidal_Value) AS ms_curHomicidal_Value
                                               -- ASMNT SA MH SCREEN
                                               , MAX(rose_asmnt_sa_mh_screen.samh_self_harm_hx) AS samh_self_harm_hx
                                               , MAX(rose_asmnt_sa_mh_screen.samh_self_harm_risk_Value) AS samh_self_harm_risk_Value
                                               -- ASMNT PSHXSR
                                               , MAX(rose_asmnt_pshxsr.pshxsr_trauma_mr_Value) AS pshxsr_trauma_mr_Value
                                               -- ASMNT BM SCREEN
                                               , MAX(rose_asmnt_bm_screen.bms_curMeds_yn) AS bms_curMeds_yn
                                               , MAX(rose_asmnt_bm_screen.bms_curMeds_co) AS bms_curMeds_co
                                               , MAX(rose_asmnt_bm_screen.bms_dcMeds_yn) AS bms_dcMeds_yn
                                               , MAX(rose_asmnt_bm_screen.bms_hspt_sr) AS bms_hspt_sr
                                               , MAX(rose_asmnt_bm_screen.bms_proced_yn) AS bms_proced_yn
                                               , MAX(rose_asmnt_bm_screen.bms_pe_time_sr_Value) AS bms_pe_time_sr_Value
                                               , MAX(rose_asmnt_bm_screen.bms_pe_time_nstxt) AS bms_pe_time_nstxt
                                               , MAX(rose_asmnt_bm_screen.bms_appt_sr) AS bms_appt_sr
                                               , MAX(rose_asmnt_bm_screen.bms_contagion_mr_Value) AS bms_contagion_mr_Value
                                               -- ASMNT LGL HX
                                               , MAX(rose_asmnt_lgl_hx.yn_legal) AS yn_legal
                                               , MAX(rose_asmnt_lgl_hx.yn_lglHx) AS yn_lglHx
                                               , MAX(rose_asmnt_lgl_hx.yn_courtOrder_Value) AS yn_courtOrder_Value
                                               , MAX(rose_asmnt_lgl_hx.yn_records_Value) AS yn_records_Value
                                        FROM ROSE.rose_asmnt_single_form
                                        LEFT OUTER JOIN ROSE.rose_asmnt_pppe ON rose_asmnt_single_form.FACILITY = rose_asmnt_pppe.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_pppe.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_pppe.EPISODE_NUMBER    
                                            AND rose_asmnt_single_form.CUSTHCDH_UID = rose_asmnt_pppe.CUSTHCDH_UID
                                        LEFT OUTER JOIN ROSE.rose_asmnt_mh_status ON rose_asmnt_single_form.FACILITY = rose_asmnt_mh_status.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_mh_status.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_mh_status.EPISODE_NUMBER
                                            AND rose_asmnt_single_form.CUSTHCET_UID = rose_asmnt_mh_status.CUSTHCET_UID 
                                        LEFT OUTER JOIN ROSE.rose_asmnt_drugsofchoice ON rose_asmnt_single_form.FACILITY = rose_asmnt_drugsofchoice.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_drugsofchoice.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_drugsofchoice.EPISODE_NUMBER
                                            AND rose_asmnt_single_form.CUSTHCCU_UID = rose_asmnt_drugsofchoice.CUSTHCCU_UID
                                        LEFT OUTER JOIN ROSE.rose_asmnt_withdrawal ON rose_asmnt_single_form.FACILITY = rose_asmnt_withdrawal.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_withdrawal.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_withdrawal.EPISODE_NUMBER
                                            AND rose_asmnt_single_form.CUSTHNFM_UID = rose_asmnt_withdrawal.CUSTHNFM_UID
                                        LEFT OUTER JOIN ROSE.rose_asmnt_txhx_primary ON rose_asmnt_single_form.FACILITY = rose_asmnt_txhx_primary.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_txhx_primary.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_txhx_primary.EPISODE_NUMBER
                                            AND rose_asmnt_single_form.CUSTHCCR_UID = rose_asmnt_txhx_primary.CUSTHCCR_UID
                                            AND rose_asmnt_single_form.CUSTHCCS_UID = rose_asmnt_txhx_primary.CUSTHCCS_UID
                                        LEFT OUTER JOIN ROSE.rose_asmnt_ismart ON rose_asmnt_single_form.FACILITY = rose_asmnt_ismart.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_ismart.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_ismart.EPISODE_NUMBER
                                            AND rose_asmnt_single_form.CUSTHNXJ_UID = rose_asmnt_ismart.CUSTHNXJ_UID
                                        LEFT OUTER JOIN ROSE.rose_asmnt_mhtxhx_primary ON rose_asmnt_single_form.FACILITY = rose_asmnt_mhtxhx_primary.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_mhtxhx_primary.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_mhtxhx_primary.EPISODE_NUMBER 
                                            AND rose_asmnt_single_form.CUSTHCCS_UID = rose_asmnt_mhtxhx_primary.CUSTHCCS_UID
                                        LEFT OUTER JOIN ROSE.rose_asmnt_sa_mh_screen ON rose_asmnt_single_form.FACILITY = rose_asmnt_sa_mh_screen.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_sa_mh_screen.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_sa_mh_screen.EPISODE_NUMBER
                                            AND rose_asmnt_single_form.CUSTHCDI_UID = rose_asmnt_sa_mh_screen.CUSTHCDI_UID
                                        LEFT OUTER JOIN ROSE.rose_asmnt_pshxsr ON rose_asmnt_single_form.FACILITY = rose_asmnt_pshxsr.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_pshxsr.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_pshxsr.EPISODE_NUMBER
                                            AND rose_asmnt_single_form.CUSTHCEX_UID = rose_asmnt_pshxsr.CUSTHCEX_UID
                                        LEFT OUTER JOIN ROSE.rose_asmnt_bm_screen ON rose_asmnt_single_form.FACILITY = rose_asmnt_bm_screen.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_bm_screen.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_bm_screen.EPISODE_NUMBER
                                            AND rose_asmnt_single_form.CUSTHCEU_UID = rose_asmnt_bm_screen.CUSTHCEU_UID
                                        LEFT OUTER JOIN ROSE.rose_asmnt_lgl_hx ON rose_asmnt_single_form.FACILITY = rose_asmnt_lgl_hx.FACILITY 
                                            AND rose_asmnt_single_form.PATID = rose_asmnt_lgl_hx.PATID 
                                            AND rose_asmnt_single_form.EPISODE_NUMBER = rose_asmnt_lgl_hx.EPISODE_NUMBER
                                            AND rose_asmnt_single_form.CUSTHNFN_UID = rose_asmnt_lgl_hx.CUSTHNFN_UID
                                        WHERE rose_asmnt_single_form.FACILITY =?
                                        AND rose_asmnt_single_form.PATID =?
                                        {episode}                                                                              
                                        ORDER BY rose_asmnt_single_form.DATA_ENTRY_DATE DESC, TO_TIMESTAMP(rose_asmnt_single_form.DATA_ENTRY_TIME,'HH:MI:SS.FF') DESC";
            }

            CombinedAssessment combinedAssessment = new CombinedAssessment();
            using (OdbcConnection connection = new OdbcConnection(_connectionStringCollection.CWS))
            {
                OdbcCommand command = new OdbcCommand(commandString, connection);
                command.Parameters.Add(new OdbcParameter("FACILITY", facility));
                command.Parameters.Add(new OdbcParameter("PATID", patientId));                

                if (episodeNumber != 0) 
                { 
                //command.Parameters.Add(new OdbcParameter("EPISODE_NUMBER", episodeNumber));
                    episode = "AND rose_asmnt_single_form.EPISODE_NUMBER = " + episodeNumber;
                    logger.Debug("Episode Number for query parameter is: " + episodeNumber);
                }
                else
                {
                    episode = "";
                    logger.Debug("There is no episode number so default to: " + episodeNumber);

                }
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                combinedAssessment.PppeProblem = GetStringValue(reader, "pppe_problem");
                                combinedAssessment.PppeDuration = GetStringValue(reader, "pppe_duration");
                                combinedAssessment.PppeSeverity = GetStringValue(reader, "pppe_severity");
                                combinedAssessment.PppeOthersSxCo = GetStringValue(reader, "pppe_othersx_co");
                                combinedAssessment.PppeSa = GetStringValue(reader, "pppe_sa");//B4
                                combinedAssessment.PppeMh = GetStringValue(reader, "pppe_mh");

                                combinedAssessment.MhsFamCo = GetStringValue(reader, "mhs_fam_co");

                                combinedAssessment.DocSupport = GetStringValue(reader, "doc_support");
                                combinedAssessment.DocFamPerception = GetStringValue(reader, "doc_fam_perception");
                                combinedAssessment.DocGenSubUseHx = GetStringValue(reader, "doc_gen_subusehx");
                                combinedAssessment.DocStageofChangeValue = GetStringValue(reader, "doc_stage_of_change_Value");
                                combinedAssessment.Doc1DocValue = GetStringValue(reader, "doc1_doc_Value");
                                combinedAssessment.SsDoc1IAValue = GetStringValue(reader, "ss_doc1_ia_Value");  //F8-IA
                                combinedAssessment.Doc1Age1Use = GetStringValue(reader, "doc1_age1use");
                                //combinedAssessment.Doc1DateUsed = DateTime.Parse(reader.GetString(reader.GetOrdinal("doc1_dateused"))).ToShortDateString(); 
                                combinedAssessment.Doc1DateUsed = DateTime.TryParse(reader["doc1_dateused"].ToString(), out DateTime result) ? reader["doc1_dateused"].ToString() : "";
                                logger.Debug("doc1_dateused .ToString() value is: " + reader["doc1_dateused"].ToString());
                                logger.Debug("doc1_dateused reader[doc1_dateused] value is: " + reader["doc1_dateused"]);
                                combinedAssessment.Doc1FreqValue = GetStringValue(reader, "doc1_freq_Value");
                                combinedAssessment.Doc1RouteValue = GetStringValue(reader, "doc1_route_Value");
                                combinedAssessment.Doc1SxValue = GetStringValue(reader, "doc1_sx_Value");

                                combinedAssessment.WithdrawalAuditoryValue = GetStringValue(reader, "ss_auditory_Value");
                                combinedAssessment.WithdrawalDiaphoresisValue = GetStringValue(reader, "ss_diaphoresis_Value");
                                combinedAssessment.WithdrawalDiarrheaValue = GetStringValue(reader, "ss_diarrhea_Value");
                                combinedAssessment.WithdrawalNauseaVomitingValue = GetStringValue(reader, "ss_nausVom_Value");
                                combinedAssessment.WithdrawalSeizureValue = GetStringValue(reader, "ss_seizure_Value");
                                combinedAssessment.WithdrawalTactileValue = GetStringValue(reader, "ss_tactile_Value");
                                combinedAssessment.WithdrawalTremorsValue = GetStringValue(reader, "ss_tremors_Value");
                                combinedAssessment.WithdrawalVisualValue = GetStringValue(reader, "ss_visual_Value");
                                combinedAssessment.WithdrawalMsOrientedValue = GetStringValue(reader, "ms_oriented_Value");
                                combinedAssessment.WithdrawalMsSymptomsValue = GetStringValue(reader, "ms_symptoms_Value");

                                combinedAssessment.TxHxPriSaEvaluation = GetStringValue(reader, "txhxp_eval_yn");
                                combinedAssessment.TxHxPriSaTxHx = GetStringValue(reader, "txhxp_txhx_yn");
                                combinedAssessment.TxHxPriSaTxHxNum = GetIntValue(reader, "txhxp_txhx_num");
                                combinedAssessment.NuPriorTxAdmit = GetIntValue(reader, "nu_prior_tx_admit");
                                combinedAssessment.NuNonTxSAHosp = GetIntValue(reader, "nu_nonTxSA_hosp");//L14-IA-A
                                combinedAssessment.TxHxPriSaSelfHelp = GetStringValue(reader, "txhxp_selfhelp_yn");
                                combinedAssessment.TxHxPriSaSelfHelpNo = GetIntValue(reader, "txhxp_selfhelp_no");

                                combinedAssessment.MhTxHxPriPriorEvaluation = GetStringValue(reader, "mhtxhx_prior_eval");
                                combinedAssessment.MhTxHxPriPriorTx = GetStringValue(reader, "mhtxhx_prior_tx");

                                combinedAssessment.SingleFormMsHxHomicidalValue = GetStringValue(reader, "ms_hxHomicidal_Value");
                                combinedAssessment.SingleFormMsCurHomicidalValue = GetStringValue(reader, "ms_curHomicidal_Value");

                                combinedAssessment.SaMhSelfHarmHistory = GetStringValue(reader, "samh_self_harm_hx");
                                combinedAssessment.SaMhScreenSelfHarmRiskValue = GetStringValue(reader, "samh_self_harm_risk_Value");
                                combinedAssessment.PshxsrTraumaMrValue = GetStringValue(reader, "pshxsr_trauma_mr_Value");

                                combinedAssessment.BmsCurMeds = GetStringValue(reader, "bms_curMeds_yn");
                                combinedAssessment.BmsCurMedsCo = GetStringValue(reader, "bms_curMeds_co");
                                combinedAssessment.BmsDcMeds = GetStringValue(reader, "bms_dcMeds_yn");
                                combinedAssessment.BmsHsptSr = GetStringValue(reader, "bms_hspt_sr");
                                combinedAssessment.BmsProcedure = GetStringValue(reader, "bms_proced_yn");
                                combinedAssessment.BmsPeTimeSrValue = GetStringValue(reader, "bms_pe_time_sr_Value");
                                combinedAssessment.BmsPeTimeNsTxt = GetStringValue(reader, "bms_pe_time_nstxt");
                                combinedAssessment.BmsApptSr = GetStringValue(reader, "bms_appt_sr");
                                combinedAssessment.BmsContagionMrValue = GetStringValue(reader, "bms_contagion_mr_Value");
                                logger.Debug("bms_contagion_mr_Value  value is: " + reader["bms_contagion_mr_Value"].ToString());
                                combinedAssessment.LegalHxYn = GetStringValue(reader, "yn_legal");
                                combinedAssessment.LegalHxYnLglHx = GetStringValue(reader, "yn_lglHx");
                                combinedAssessment.LegalCourtOrderValue = GetStringValue(reader, "yn_courtOrder_Value");
                                combinedAssessment.LegalRecordsValue = GetStringValue(reader, "yn_records_Value");
                            }
                        }
                    }
                }
                catch (OdbcException ex)
                {
                    logger.Error(ex, "GetClientCombinedAssessmentInfoByPatientId: Could not connect to ODBC data source. See error message. Data Source: {systemDsn}. Error: {errorMessage}", _connectionStringCollection.CWS, ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "GetClientCombinedAssessmentInfoByPatientId: An unexpected error occurred. Error Type: {errorType}. Error: {errorMessage}", ex.GetType(), ex.Message);
                    throw;
                }
            }
            return combinedAssessment;
        }
    }
}
