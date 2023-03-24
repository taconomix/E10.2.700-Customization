/*== QuoteDtl_UD => OrderDtl_UD ==============================================
    
    Table: OrderDtl (In-Transaction)

    Created: 02/15/20232 -Kevin Veldman
    Changed: 

    Info: Data Directive - When creating new SO from quote, copy all UD fields from quoteDtl to matching UD field in OrderDtl

============================================================================*/

foreach ( var od in ttOrderDtl.Where(x => x.RowMod=="A" && x.QuoteNum!=0) ) {

    var qd = Db.QuoteDtl.Where(x => x.Company == CompanyID
        && x.QuoteNum  == od.QuoteNum
        && x.QuoteLine == od.QuoteLine)
    .FirstOrDefault();

    if ( od != null && qd != null ) {

        Action<string> udCopy = col => {
            if ( col.EndsWith("_c") ) od[col] = qd[col];
        };

        qd.GetType().GetProperties().Select(s => s.Name).ToList().ForEach(udCopy);
    }
}


/*== CHANGE LOG ==============================================================

============================================================================*/







/* UD Fields Hard-coded

foreach ( var od in ttOrderDtl.Where(x => x.RowMod == "A" && x.QuoteNum != 0) ) {

    var qd = Db.QuoteDtl.Where(x => x.Company == CompanyID
        && x.QuoteNum  == od.QuoteNum
        && x.QuoteLine == od.QuoteLine)
    .FirstOrDefault();

    if ( od != null && qd != null ) {

        od["cAnkleMods_c"] = qd["cAnkleMods_c"];
        od["cAnkleType_c"] = qd["cAnkleType_c"];
        od["cAntShell_c"] = qd["cAntShell_c"];
        od["cBoot_c"] = qd["cBoot_c"];
        od["cBootHt_c"] = qd["cBootHt_c"];
        od["cBootLen_c"] = qd["cBootLen_c"];
        od["cChafeType_c"] = qd["cChafeType_c"];
        od["cCondExtML_c"] = qd["cCondExtML_c"];
        od["cCondPadML_c"] = qd["cCondPadML_c"];
        od["cDeviceCode_c"] = qd["cDeviceCode_c"];
        od["cDrsmWings_c"] = qd["cDrsmWings_c"];
        od["cEvalCO_c"] = qd["cEvalCO_c"];
        od["cFormNotes_c"] = qd["cFormNotes_c"];
        od["cFPCover_c"] = qd["cFPCover_c"];
        od["cGillPos_c"] = qd["cGillPos_c"];
        od["cHeelPost_c"] = qd["cHeelPost_c"];
        od["cHingeAL_c"] = qd["cHingeAL_c"];
        od["cHingeAM_c"] = qd["cHingeAM_c"];
        od["cHingeKL_c"] = qd["cHingeKL_c"];
        od["cHingeKM_c"] = qd["cHingeKM_c"];
        od["cInternalNotes_c"] = qd["cInternalNotes_c"];
        od["cIntlCustomsCode_c"] = qd["cIntlCustomsCode_c"];
        od["cKneeMod_c"] = qd["cKneeMod_c"];
        od["cLinerHt_c"] = qd["cLinerHt_c"];
        od["cLinerMtl_c"] = qd["cLinerMtl_c"];
        od["cLtHip_c"] = qd["cLtHip_c"];
        od["cModNotes_c"] = qd["cModNotes_c"];
        od["cMold1_c"] = qd["cMold1_c"];
        od["cMold2_c"] = qd["cMold2_c"];
        od["cMold3_c"] = qd["cMold3_c"];
        od["cOpenPos_c"] = qd["cOpenPos_c"];
        od["cPattern_c"] = qd["cPattern_c"];
        od["cPlasticDuro_c"] = qd["cPlasticDuro_c"];
        od["cPlasticType_c"] = qd["cPlasticType_c"];
        od["cReinforceType_c"] = qd["cReinforceType_c"];
        od["cRmkCO_c"] = qd["cRmkCO_c"];
        od["cRmkRsnAct_c"] = qd["cRmkRsnAct_c"];
        od["cRmkRsnReq_c"] = qd["cRmkRsnReq_c"];
        od["cRtHip_c"] = qd["cRtHip_c"];
        od["cSaboTrimML_c"] = qd["cSaboTrimML_c"];
        od["cSBLR_c"] = qd["cSBLR_c"];
        od["cShoeSize_c"] = qd["cShoeSize_c"];
        od["cSMOmtl_c"] = qd["cSMOmtl_c"];
        od["cSpecAL_c"] = qd["cSpecAL_c"];
        od["cSpecAM_c"] = qd["cSpecAM_c"];
        od["cSpecKL_c"] = qd["cSpecKL_c"];
        od["cSpecKM_c"] = qd["cSpecKM_c"];
        od["cSsmlAlt1_c"] = qd["cSsmlAlt1_c"];
        od["cSsmlUnit_c"] = qd["cSsmlUnit_c"];
        od["cStopType_c"] = qd["cStopType_c"];
        od["cStrapColor_c"] = qd["cStrapColor_c"];
        od["cStrapType_c"] = qd["cStrapType_c"];
        od["cTStrapML_c"] = qd["cTStrapML_c"];
        od["cTstrpMtl_c"] = qd["cTstrpMtl_c"];
        od["dAnkleMod_c"] = qd["dAnkleMod_c"];
        od["dAPatASIS_c"] = qd["dAPatASIS_c"];
        od["dAPatAxla_c"] = qd["dAPatAxla_c"];
        od["dAPatLowRib_c"] = qd["dAPatLowRib_c"];
        od["dAPatNipLn_c"] = qd["dAPatNipLn_c"];
        od["dAPatTroch_c"] = qd["dAPatTroch_c"];
        od["dAPatWaist_c"] = qd["dAPatWaist_c"];
        od["dAPatXyphd_c"] = qd["dAPatXyphd_c"];
        od["dAsisML_c"] = qd["dAsisML_c"];
        od["dC7toInfScapLEN_c"] = qd["dC7toInfScapLEN_c"];
        od["dCarbonQty_c"] = qd["dCarbonQty_c"];
        od["dCRCatASIS_c"] = qd["dCRCatASIS_c"];
        od["dCRCatAxla_c"] = qd["dCRCatAxla_c"];
        od["dCRCatHip_c"] = qd["dCRCatHip_c"];
        od["dCRCatLowRib_c"] = qd["dCRCatLowRib_c"];
        od["dCRCatNipLn_c"] = qd["dCRCatNipLn_c"];
        od["dCRCatTroch_c"] = qd["dCRCatTroch_c"];
        od["dCRCatWaist_c"] = qd["dCRCatWaist_c"];
        od["dCRCatXyphd_c"] = qd["dCRCatXyphd_c"];
        od["dDist2CRC_c"] = qd["dDist2CRC_c"];
        od["dDist2ML_c"] = qd["dDist2ML_c"];
        od["dDist4CRC_c"] = qd["dDist4CRC_c"];
        od["dDist4ML_c"] = qd["dDist4ML_c"];
        od["dDist6CRC_c"] = qd["dDist6CRC_c"];
        od["dDist6ML_c"] = qd["dDist6ML_c"];
        od["dDist8CRC_c"] = qd["dDist8CRC_c"];
        od["dDist8ML_c"] = qd["dDist8ML_c"];
        od["dForeMod_c"] = qd["dForeMod_c"];
        od["dGlabAP_c"] = qd["dGlabAP_c"];
        od["dGlabCRC_c"] = qd["dGlabCRC_c"];
        od["dGlabML_c"] = qd["dGlabML_c"];
        od["dHeelMod_c"] = qd["dHeelMod_c"];
        od["dInfScapCRC_c"] = qd["dInfScapCRC_c"];
        od["dKC2Low_c"] = qd["dKC2Low_c"];
        od["dKC2Top_c"] = qd["dKC2Top_c"];
        od["dKCmidAP_c"] = qd["dKCmidAP_c"];
        od["dKCmidCRC_c"] = qd["dKCmidCRC_c"];
        od["dKCmidML_c"] = qd["dKCmidML_c"];
        od["dKCtoPrnm_c"] = qd["dKCtoPrnm_c"];
        od["dKneePadBuckles_c"] = qd["dKneePadBuckles_c"];
        od["dKneeValgum_c"] = qd["dKneeValgum_c"];
        od["dKneeVarum_c"] = qd["dKneeVarum_c"];
        od["dKyphosis_c"] = qd["dKyphosis_c"];
        od["dLiftHt_c"] = qd["dLiftHt_c"];
        od["dLordosis_c"] = qd["dLordosis_c"];
        od["dLowAP_c"] = qd["dLowAP_c"];
        od["dMeas1_c"] = qd["dMeas1_c"];
        od["dMeas10_c"] = qd["dMeas10_c"];
        od["dMeas10r_c"] = qd["dMeas10r_c"];
        od["dMeas11_c"] = qd["dMeas11_c"];
        od["dMeas11r_c"] = qd["dMeas11r_c"];
        od["dMeas12_c"] = qd["dMeas12_c"];
        od["dMeas12r_c"] = qd["dMeas12r_c"];
        od["dMeas13_c"] = qd["dMeas13_c"];
        od["dMeas13r_c"] = qd["dMeas13r_c"];
        od["dMeas1r_c"] = qd["dMeas1r_c"];
        od["dMeas2_c"] = qd["dMeas2_c"];
        od["dMeas2r_c"] = qd["dMeas2r_c"];
        od["dMeas3_c"] = qd["dMeas3_c"];
        od["dMeas3r_c"] = qd["dMeas3r_c"];
        od["dMeas4_c"] = qd["dMeas4_c"];
        od["dMeas4r_c"] = qd["dMeas4r_c"];
        od["dMeas5_c"] = qd["dMeas5_c"];
        od["dMeas5r_c"] = qd["dMeas5r_c"];
        od["dMeas6_c"] = qd["dMeas6_c"];
        od["dMeas6r_c"] = qd["dMeas6r_c"];
        od["dMeas7_c"] = qd["dMeas7_c"];
        od["dMeas7r_c"] = qd["dMeas7r_c"];
        od["dMeas8_c"] = qd["dMeas8_c"];
        od["dMeas8r_c"] = qd["dMeas8r_c"];
        od["dMeas9_c"] = qd["dMeas9_c"];
        od["dMeas9r_c"] = qd["dMeas9r_c"];
        od["dMLatASIS_c"] = qd["dMLatASIS_c"];
        od["dMLatAxla_c"] = qd["dMLatAxla_c"];
        od["dMLatHip_c"] = qd["dMLatHip_c"];
        od["dMLatLowRib_c"] = qd["dMLatLowRib_c"];
        od["dMLatNipLn_c"] = qd["dMLatNipLn_c"];
        od["dMLatTroch_c"] = qd["dMLatTroch_c"];
        od["dMLatWaist_c"] = qd["dMLatWaist_c"];
        od["dMLatXyphd_c"] = qd["dMLatXyphd_c"];
        od["dOxptToInfScapLEN_c"] = qd["dOxptToInfScapLEN_c"];
        od["dPatientHt_c"] = qd["dPatientHt_c"];
        od["dPatientWt_c"] = qd["dPatientWt_c"];
        od["dPelvicBand_c"] = qd["dPelvicBand_c"];
        od["dProx2CRC_c"] = qd["dProx2CRC_c"];
        od["dProx2ML_c"] = qd["dProx2ML_c"];
        od["dProx4CRC_c"] = qd["dProx4CRC_c"];
        od["dProx4ML_c"] = qd["dProx4ML_c"];
        od["dProx6CRC_c"] = qd["dProx6CRC_c"];
        od["dProx6ML_c"] = qd["dProx6ML_c"];
        od["dProx8CRC_c"] = qd["dProx8CRC_c"];
        od["dProx8ML_c"] = qd["dProx8ML_c"];
        od["dReinforceDuro_c"] = qd["dReinforceDuro_c"];
        od["dScapWaistLEN_c"] = qd["dScapWaistLEN_c"];
        od["dStrnmXyphdLEN_c"] = qd["dStrnmXyphdLEN_c"];
        od["dStrutHT_c"] = qd["dStrutHT_c"];
        od["dStrutWidth_c"] = qd["dStrutWidth_c"];
        od["dSympAsisLEN_c"] = qd["dSympAsisLEN_c"];
        od["dTopAP_c"] = qd["dTopAP_c"];
        od["dtPatientDOB_c"] = qd["dtPatientDOB_c"];
        od["dTrochToWaist_c"] = qd["dTrochToWaist_c"];
        od["dTrochWaistLEN_c"] = qd["dTrochWaistLEN_c"];
        od["dUprights_c"] = qd["dUprights_c"];
        od["dWaistAxlaLEN_c"] = qd["dWaistAxlaLEN_c"];
        od["dWaistCRC_c"] = qd["dWaistCRC_c"];
        od["dWaistML_c"] = qd["dWaistML_c"];
        od["dWaistStrnmLEN_c"] = qd["dWaistStrnmLEN_c"];
        od["dWaistSympLEN_c"] = qd["dWaistSympLEN_c"];
        od["dWedgeQty_c"] = qd["dWedgeQty_c"];
        od["Dx_c"] = qd["Dx_c"];
        od["EmailOForm_c"] = qd["EmailOForm_c"];
        od["FirstName_c"] = qd["FirstName_c"];
        od["ForeignSysRowID"] = qd["ForeignSysRowID"];
        od["Gender_c"] = qd["Gender_c"];
        od["kAFOFC_c"] = qd["kAFOFC_c"];
        od["kAnkleBuildup_c"] = qd["kAnkleBuildup_c"];
        od["kAntFlap_c"] = qd["kAntFlap_c"];
        od["kAntGill_c"] = qd["kAntGill_c"];
        od["kAntOpen_c"] = qd["kAntOpen_c"];
        od["kBACPump_c"] = qd["kBACPump_c"];
        od["kBivalve_c"] = qd["kBivalve_c"];
        od["kBlackGauntlet_c"] = qd["kBlackGauntlet_c"];
        od["kBoot_c"] = qd["kBoot_c"];
        od["kBSHoles_c"] = qd["kBSHoles_c"];
        od["kCarbonStrut_c"] = qd["kCarbonStrut_c"];
        od["kCFplate_c"] = qd["kCFplate_c"];
        od["kClubfoot_c"] = qd["kClubfoot_c"];
        od["kDacronStrap_c"] = qd["kDacronStrap_c"];
        od["kDiabInsert_c"] = qd["kDiabInsert_c"];
        od["kDorsalChip_c"] = qd["kDorsalChip_c"];
        od["kExtMedWall_c"] = qd["kExtMedWall_c"];
        od["kExtraMods_c"] = qd["kExtraMods_c"];
        od["kFullFP_c"] = qd["kFullFP_c"];
        od["kGills_c"] = qd["kGills_c"];
        od["kGTube_c"] = qd["kGTube_c"];
        od["kGusset_c"] = qd["kGusset_c"];
        od["kHeelCut_c"] = qd["kHeelCut_c"];
        od["kHeelPost_c"] = qd["kHeelPost_c"];
        od["kHKAFO_c"] = qd["kHKAFO_c"];
        od["kHoudini_c"] = qd["kHoudini_c"];
        od["kIndyStrap_c"] = qd["kIndyStrap_c"];
        od["kKAFO_c"] = qd["kKAFO_c"];
        od["kKIEUmtls_c"] = qd["kKIEUmtls_c"];
        od["kLatGill_c"] = qd["kLatGill_c"];
        od["kLiner_c"] = qd["kLiner_c"];
        od["kMetPads_c"] = qd["kMetPads_c"];
        od["kMtlUpgrade_c"] = qd["kMtlUpgrade_c"];
        od["kNightStrap_c"] = qd["kNightStrap_c"];
        od["kNonSkid_c"] = qd["kNonSkid_c"];
        od["kPartTrap_c"] = qd["kPartTrap_c"];
        od["kPlateau_c"] = qd["kPlateau_c"];
        od["kPowderCoat_c"] = qd["kPowderCoat_c"];
        od["kPrepped_c"] = qd["kPrepped_c"];
        od["kPstOpen_c"] = qd["kPstOpen_c"];
        od["kQuickRelease_c"] = qd["kQuickRelease_c"];
        od["kRemake_c"] = qd["kRemake_c"];
        od["kReturnCast_c"] = qd["kReturnCast_c"];
        od["kRevTrim_c"] = qd["kRevTrim_c"];
        od["kRGO_c"] = qd["kRGO_c"];
        od["kRush0D_c"] = qd["kRush0D_c"];
        od["kRush1D_c"] = qd["kRush1D_c"];
        od["kRushNC_c"] = qd["kRushNC_c"];
        od["kSoftee_c"] = qd["kSoftee_c"];
        od["kSplitMeas_c"] = qd["kSplitMeas_c"];
        od["kSSplate_c"] = qd["kSSplate_c"];
        od["kSTPads_c"] = qd["kSTPads_c"];
        od["kTallEars_c"] = qd["kTallEars_c"];
        od["kTaperBoot_c"] = qd["kTaperBoot_c"];
        od["kToeWalk_c"] = qd["kToeWalk_c"];
        od["kTwoLiners_c"] = qd["kTwoLiners_c"];
        od["kValidationPass_c"] = qd["kValidationPass_c"];
        od["kWalkingBase_c"] = qd["kWalkingBase_c"];
        od["kWedge_c"] = qd["kWedge_c"];
        od["LastNamePID_c"] = qd["LastNamePID_c"];
        od["ModType_c"] = qd["ModType_c"];
        od["OrderNotes_c"] = qd["OrderNotes_c"];
        od["OriginalSO_c"] = qd["OriginalSO_c"];
        od["PTUserEmail_c"] = qd["PTUserEmail_c"];
        od["ShipCode_c"] = qd["ShipCode_c"];
    }
} */