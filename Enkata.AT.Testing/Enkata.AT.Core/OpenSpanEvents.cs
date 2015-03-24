using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using NUnit.Framework;
using White.Core;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowItems;


namespace Enkata.ActivityTracker.Core
{
    public static class OpenSpanEvents
    {
        // List of all hotkeys used in the scenarios
        public static OpenSpan F1_hotkey;
        public static OpenSpan F2_hotkey;
        public static OpenSpan F3_hotkey;
        public static OpenSpan F4_hotkey;
        public static OpenSpan F5_hotkey;
        public static OpenSpan F6_hotkey;
        public static OpenSpan F7_hotkey;
        public static OpenSpan F8_hotkey;
        public static OpenSpan F9_hotkey;
        public static OpenSpan F10_hotkey;
        public static OpenSpan F11_hotkey;
        public static OpenSpan F12_hotkey;
        public static OpenSpan O_hotkey;
        public static OpenSpan AltA_hotkey;
        public static OpenSpan AltC_hotkey;
        public static OpenSpan AltG_hotkey;
        public static OpenSpan AltH_hotkey;
        public static OpenSpan AltI_hotkey;
        public static OpenSpan AltM_hotkey;
        public static OpenSpan AltO_hotkey;
        public static OpenSpan AltP_hotkey;
        public static OpenSpan AltR_hotkey;
        public static OpenSpan CtrlM_hotkey;
        public static OpenSpan ShiftF4_hotkey;

        public static OpenSpan H_hotkey;
        public static OpenSpan I_hotkey;
        public static OpenSpan M_hotkey;
        public static OpenSpan Tab_hotkey;
        public static OpenSpan Enter_hotkey;

        //List of all Macros in Claim Demo Tab
        public static OpenSpan F1_macro_ClaimDemo;
        public static OpenSpan F2_macro_ClaimDemo;
        public static OpenSpan F3_macro_ClaimDemo;
        public static OpenSpan F4_macro_ClaimDemo;
        public static OpenSpan F5_macro_ClaimDemo;
        public static OpenSpan F6_macro_ClaimDemo;
        public static OpenSpan F7_macro_ClaimDemo;
        public static OpenSpan F8_macro_ClaimDemo;
        public static OpenSpan F9_macro_ClaimDemo;
        public static OpenSpan F10_macro_ClaimDemo;
        public static OpenSpan F11_macro_ClaimDemo;
        public static OpenSpan F12_macro_ClaimDemo;

        //List of all Macros in General Tab
        public static OpenSpan F1_macro_General;
        public static OpenSpan F2_macro_General;
        public static OpenSpan F3_macro_General;
        public static OpenSpan F4_macro_General;
        public static OpenSpan F5_macro_General;
        public static OpenSpan F6_macro_General;
        public static OpenSpan F7_macro_General;
        public static OpenSpan F8_macro_General;
        public static OpenSpan F9_macro_General;
        public static OpenSpan F10_macro_General;
        public static OpenSpan F11_macro_General;
        public static OpenSpan F12_macro_General;

        //List of all Macros in History Tab
        public static OpenSpan F1_macro_History;
        public static OpenSpan F2_macro_History;
        public static OpenSpan F3_macro_History;
        public static OpenSpan F4_macro_History;
        public static OpenSpan F5_macro_History;
        public static OpenSpan F6_macro_History;
        public static OpenSpan F7_macro_History;
        public static OpenSpan F8_macro_History;
        public static OpenSpan F9_macro_History;
        public static OpenSpan F10_macro_History;
        public static OpenSpan F11_macro_History;
        public static OpenSpan F12_macro_History;

        //List of all Macros in Reimbursement Tab
        public static OpenSpan F1_macro_Reimbursement;
        public static OpenSpan F2_macro_Reimbursement;
        public static OpenSpan F3_macro_Reimbursement;
        public static OpenSpan F4_macro_Reimbursement;
        public static OpenSpan F5_macro_Reimbursement;
        public static OpenSpan F6_macro_Reimbursement;
        public static OpenSpan F7_macro_Reimbursement;
        public static OpenSpan F8_macro_Reimbursement;
        public static OpenSpan F9_macro_Reimbursement;
        public static OpenSpan F10_macro_Reimbursement;
        public static OpenSpan F11_macro_Reimbursement;
        public static OpenSpan F12_macro_Reimbursement;

        //List of all Macros in COB Tab
        public static OpenSpan F1_macro_COB;
        public static OpenSpan F2_macro_COB;
        public static OpenSpan F3_macro_COB;
        public static OpenSpan F4_macro_COB;
        public static OpenSpan F5_macro_COB;
        public static OpenSpan F6_macro_COB;
        public static OpenSpan F7_macro_COB;
        public static OpenSpan F8_macro_COB;
        public static OpenSpan F9_macro_COB;
        public static OpenSpan F10_macro_COB;
        public static OpenSpan F11_macro_COB;
        public static OpenSpan F12_macro_COB;

        //List of all Macros in Provider Tab
        public static OpenSpan F1_macro_Provider;
        public static OpenSpan F2_macro_Provider;
        public static OpenSpan F3_macro_Provider;
        public static OpenSpan F4_macro_Provider;
        public static OpenSpan F5_macro_Provider;
        public static OpenSpan F6_macro_Provider;
        public static OpenSpan F7_macro_Provider;
        public static OpenSpan F8_macro_Provider;
        public static OpenSpan F9_macro_Provider;
        public static OpenSpan F10_macro_Provider;
        public static OpenSpan F11_macro_Provider;
        public static OpenSpan F12_macro_Provider;

        //List of all Macros in PreCertification Tab
        public static OpenSpan F1_macro_PreCertification;
        public static OpenSpan F2_macro_PreCertification;
        public static OpenSpan F3_macro_PreCertification;
        public static OpenSpan F4_macro_PreCertification;
        public static OpenSpan F5_macro_PreCertification;
        public static OpenSpan F6_macro_PreCertification;
        public static OpenSpan F7_macro_PreCertification;
        public static OpenSpan F8_macro_PreCertification;
        public static OpenSpan F9_macro_PreCertification;
        public static OpenSpan F10_macro_PreCertification;
        public static OpenSpan F11_macro_PreCertification;
        public static OpenSpan F12_macro_PreCertification;

        //List of all Macros in Medicaid Tab
        public static OpenSpan F1_macro_Medicaid;
        public static OpenSpan F2_macro_Medicaid;
        public static OpenSpan F3_macro_Medicaid;
        public static OpenSpan F4_macro_Medicaid;
        public static OpenSpan F5_macro_Medicaid;
        public static OpenSpan F6_macro_Medicaid;
        public static OpenSpan F7_macro_Medicaid;
        public static OpenSpan F8_macro_Medicaid;
        public static OpenSpan F9_macro_Medicaid;
        public static OpenSpan F10_macro_Medicaid;
        public static OpenSpan F11_macro_Medicaid;
        public static OpenSpan F12_macro_Medicaid;

        //List of all Macros in Pricing Tab
        public static OpenSpan F1_macro_Pricing;
        public static OpenSpan F2_macro_Pricing;
        public static OpenSpan F3_macro_Pricing;
        public static OpenSpan F4_macro_Pricing;
        public static OpenSpan F5_macro_Pricing;
        public static OpenSpan F6_macro_Pricing;
        public static OpenSpan F7_macro_Pricing;
        public static OpenSpan F8_macro_Pricing;
        public static OpenSpan F9_macro_Pricing;
        public static OpenSpan F10_macro_Pricing;
        public static OpenSpan F11_macro_Pricing;
        public static OpenSpan F12_macro_Pricing;

        //List of all Macros in Facility Tab
        public static OpenSpan F1_macro_Facilty;
        public static OpenSpan F2_macro_Facilty;
        public static OpenSpan F3_macro_Facilty;
        public static OpenSpan F4_macro_Facilty;
        public static OpenSpan F5_macro_Facilty;
        public static OpenSpan F6_macro_Facilty;
        public static OpenSpan F7_macro_Facilty;
        public static OpenSpan F8_macro_Facilty;
        public static OpenSpan F9_macro_Facilty;
        public static OpenSpan F10_macro_Facilty;
        public static OpenSpan F11_macro_Facilty;
        public static OpenSpan F12_macro_Facilty;

        
        public static void SetAllMacros()
        {
            //Claim Demo macros
            F1_macro_ClaimDemo = new OpenSpan();
            F1_macro_ClaimDemo.Name1 = "macro_name";
            F1_macro_ClaimDemo.Value1 = "btnF1 - Claim Demo";

            F2_macro_ClaimDemo = new OpenSpan();
            F2_macro_ClaimDemo.Name1 = "macro_name";
            F2_macro_ClaimDemo.Value1 = "btnF2 - Claim Demo";

            F3_macro_ClaimDemo = new OpenSpan();
            F3_macro_ClaimDemo.Name1 = "macro_name";
            F3_macro_ClaimDemo.Value1 = "btnF3 - ClaimDemo";

            F4_macro_ClaimDemo = new OpenSpan();
            F4_macro_ClaimDemo.Name1 = "macro_name";
            F4_macro_ClaimDemo.Value1 = "btnF4 - ClaimDemo";

            F5_macro_ClaimDemo = new OpenSpan();
            F5_macro_ClaimDemo.Name1 = "macro_name";
            F5_macro_ClaimDemo.Value1 = "btnF5 - ClaimDemo";

            F6_macro_ClaimDemo = new OpenSpan();
            F6_macro_ClaimDemo.Name1 = "macro_name";
            F6_macro_ClaimDemo.Value1 = "btnF6 - ClaimDemo";

            F7_macro_ClaimDemo = new OpenSpan();
            F7_macro_ClaimDemo.Name1 = "macro_name";
            F7_macro_ClaimDemo.Value1 = "btnF7 - ClaimDemo";

            F8_macro_ClaimDemo = new OpenSpan();
            F8_macro_ClaimDemo.Name1 = "macro_name";
            F8_macro_ClaimDemo.Value1 = "btnF8 - ClaimDemo";

            F9_macro_ClaimDemo = new OpenSpan();
            F9_macro_ClaimDemo.Name1 = "macro_name";
            F9_macro_ClaimDemo.Value1 = "btnF9 - ClaimDemo";

            F10_macro_ClaimDemo = new OpenSpan();
            F10_macro_ClaimDemo.Name1 = "macro_name";
            F10_macro_ClaimDemo.Value1 = "btnF10 - ClaimDemo";

            F11_macro_ClaimDemo = new OpenSpan();
            F11_macro_ClaimDemo.Name1 = "macro_name";
            F11_macro_ClaimDemo.Value1 = "btnF11 - ClaimDemo";

            F12_macro_ClaimDemo = new OpenSpan();
            F12_macro_ClaimDemo.Name1 = "macro_name";
            F12_macro_ClaimDemo.Value1 = "btnF12 - ClaimDemo";

            //General Macros
            F1_macro_General = new OpenSpan();
            F1_macro_General.Name1 = "macro_name";
            F1_macro_General.Value1 = "btnF1 - General";

            F2_macro_General = new OpenSpan();
            F2_macro_General.Name1 = "macro_name";
            F2_macro_General.Value1 = "btnF2 - General";

            F3_macro_General = new OpenSpan();
            F3_macro_General.Name1 = "macro_name";
            F3_macro_General.Value1 = "btnF3 - General";

            F4_macro_General = new OpenSpan();
            F4_macro_General.Name1 = "macro_name";
            F4_macro_General.Value1 = "btnF4 - General";

            F5_macro_General = new OpenSpan();
            F5_macro_General.Name1 = "macro_name";
            F5_macro_General.Value1 = "btnF5 - General";

            F6_macro_General = new OpenSpan();
            F6_macro_General.Name1 = "macro_name";
            F6_macro_General.Value1 = "btnF6 - General";

            F7_macro_General = new OpenSpan();
            F7_macro_General.Name1 = "macro_name";
            F7_macro_General.Value1 = "btnF7 - General";

            F8_macro_General = new OpenSpan();
            F8_macro_General.Name1 = "macro_name";
            F8_macro_General.Value1 = "btnF8 - General";

            F9_macro_General = new OpenSpan();
            F9_macro_General.Name1 = "macro_name";
            F9_macro_General.Value1 = "btnF9 - General";

            F10_macro_General = new OpenSpan();
            F10_macro_General.Name1 = "macro_name";
            F10_macro_General.Value1 = "btnF10 - General";

            F11_macro_General = new OpenSpan();
            F11_macro_General.Name1 = "macro_name";
            F11_macro_General.Value1 = "btnF11 - General";

            F12_macro_General = new OpenSpan();
            F12_macro_General.Name1 = "macro_name";
            F12_macro_General.Value1 = "btnF12 - General";

            //History Macros
            F1_macro_History = new OpenSpan();
            F1_macro_History.Name1 = "macro_name";
            F1_macro_History.Value1 = "btnF1 - History";

            F2_macro_History = new OpenSpan();
            F2_macro_History.Name1 = "macro_name";
            F2_macro_History.Value1 = "btnF2 - History";

            F3_macro_History = new OpenSpan();
            F3_macro_History.Name1 = "macro_name";
            F3_macro_History.Value1 = "btnF3 - History";

            F4_macro_History = new OpenSpan();
            F4_macro_History.Name1 = "macro_name";
            F4_macro_History.Value1 = "btnF4 - History";

            F5_macro_History = new OpenSpan();
            F5_macro_History.Name1 = "macro_name";
            F5_macro_History.Value1 = "btnF5 - History";

            F6_macro_History = new OpenSpan();
            F6_macro_History.Name1 = "macro_name";
            F6_macro_History.Value1 = "btnF6 - History";

            F7_macro_History = new OpenSpan();
            F7_macro_History.Name1 = "macro_name";
            F7_macro_History.Value1 = "btnF7 - History";

            F8_macro_History = new OpenSpan();
            F8_macro_History.Name1 = "macro_name";
            F8_macro_History.Value1 = "btnF8 - History";

            F9_macro_History = new OpenSpan();
            F9_macro_History.Name1 = "macro_name";
            F9_macro_History.Value1 = "btnF9 - History";

            F10_macro_History = new OpenSpan();
            F10_macro_History.Name1 = "macro_name";
            F10_macro_History.Value1 = "btnF10 - History";

            F11_macro_History = new OpenSpan();
            F11_macro_History.Name1 = "macro_name";
            F11_macro_History.Value1 = "btnF11 - History";

            F12_macro_History = new OpenSpan();
            F12_macro_History.Name1 = "macro_name";
            F12_macro_History.Value1 = "btnF12 - History";

            //Reimbursement Macros
            F1_macro_Reimbursement = new OpenSpan();
            F1_macro_Reimbursement.Name1 = "macro_name";
            F1_macro_Reimbursement.Value1 = "btnF1 - Reimbursement";

            F2_macro_Reimbursement = new OpenSpan();
            F2_macro_Reimbursement.Name1 = "macro_name";
            F2_macro_Reimbursement.Value1 = "btnF2 - Reimbursement";

            F3_macro_Reimbursement = new OpenSpan();
            F3_macro_Reimbursement.Name1 = "macro_name";
            F3_macro_Reimbursement.Value1 = "btnF3 - Reimbursement";

            F4_macro_Reimbursement = new OpenSpan();
            F4_macro_Reimbursement.Name1 = "macro_name";
            F4_macro_Reimbursement.Value1 = "btnF4 - Reimbursement";

            F5_macro_Reimbursement = new OpenSpan();
            F5_macro_Reimbursement.Name1 = "macro_name";
            F5_macro_Reimbursement.Value1 = "btnF5 - Reimbursement";

            F6_macro_Reimbursement = new OpenSpan();
            F6_macro_Reimbursement.Name1 = "macro_name";
            F6_macro_Reimbursement.Value1 = "btnF6 - Reimbursement";

            F7_macro_Reimbursement = new OpenSpan();
            F7_macro_Reimbursement.Name1 = "macro_name";
            F7_macro_Reimbursement.Value1 = "btnF7 - Reimbursement";

            F8_macro_Reimbursement = new OpenSpan();
            F8_macro_Reimbursement.Name1 = "macro_name";
            F8_macro_Reimbursement.Value1 = "btnF8 - Reimbursement";

            F9_macro_Reimbursement = new OpenSpan();
            F9_macro_Reimbursement.Name1 = "macro_name";
            F9_macro_Reimbursement.Value1 = "btnF9 - Reimbursement";

            F10_macro_Reimbursement = new OpenSpan();
            F10_macro_Reimbursement.Name1 = "macro_name";
            F10_macro_Reimbursement.Value1 = "btnF10 - Reimbursement";

            F11_macro_Reimbursement = new OpenSpan();
            F11_macro_Reimbursement.Name1 = "macro_name";
            F11_macro_Reimbursement.Value1 = "btnF11 - Reimbursement";

            F12_macro_Reimbursement = new OpenSpan();
            F12_macro_Reimbursement.Name1 = "macro_name";
            F12_macro_Reimbursement.Value1 = "btnF12 - Reimbursement";

            //COB Macros
            F1_macro_COB = new OpenSpan();
            F1_macro_COB.Name1 = "macro_name";
            F1_macro_COB.Value1 = "btnF1 - COB";

            F2_macro_COB = new OpenSpan();
            F2_macro_COB.Name1 = "macro_name";
            F2_macro_COB.Value1 = "btnF2 - COB";

            F3_macro_COB = new OpenSpan();
            F3_macro_COB.Name1 = "macro_name";
            F3_macro_COB.Value1 = "btnF3 - COB";

            F4_macro_COB = new OpenSpan();
            F4_macro_COB.Name1 = "macro_name";
            F4_macro_COB.Value1 = "btnF4 - COB";

            F5_macro_COB = new OpenSpan();
            F5_macro_COB.Name1 = "macro_name";
            F5_macro_COB.Value1 = "btnF5 - COB";

            F6_macro_COB = new OpenSpan();
            F6_macro_COB.Name1 = "macro_name";
            F6_macro_COB.Value1 = "btnF6 - COB";

            F7_macro_COB = new OpenSpan();
            F7_macro_COB.Name1 = "macro_name";
            F7_macro_COB.Value1 = "btnF7 - COB";

            F8_macro_COB = new OpenSpan();
            F8_macro_COB.Name1 = "macro_name";
            F8_macro_COB.Value1 = "btnF8 - COB";

            F9_macro_COB = new OpenSpan();
            F9_macro_COB.Name1 = "macro_name";
            F9_macro_COB.Value1 = "btnF9 - COB";

            F10_macro_COB = new OpenSpan();
            F10_macro_COB.Name1 = "macro_name";
            F10_macro_COB.Value1 = "btnF10 - COB";

            F11_macro_COB = new OpenSpan();
            F11_macro_COB.Name1 = "macro_name";
            F11_macro_COB.Value1 = "btnF11 - COB";

            F12_macro_COB = new OpenSpan();
            F12_macro_COB.Name1 = "macro_name";
            F12_macro_COB.Value1 = "btnF12 - COB";

            //Provider Macros
            F1_macro_Provider = new OpenSpan();
            F1_macro_Provider.Name1 = "macro_name";
            F1_macro_Provider.Value1 = "btnF1 - Provider";

            F2_macro_Provider = new OpenSpan();
            F2_macro_Provider.Name1 = "macro_name";
            F2_macro_Provider.Value1 = "btnF2 - Provider";

            F3_macro_Provider = new OpenSpan();
            F3_macro_Provider.Name1 = "macro_name";
            F3_macro_Provider.Value1 = "btnF3 - Provider";

            F4_macro_Provider = new OpenSpan();
            F4_macro_Provider.Name1 = "macro_name";
            F4_macro_Provider.Value1 = "btnF4 - Provider";

            F5_macro_Provider = new OpenSpan();
            F5_macro_Provider.Name1 = "macro_name";
            F5_macro_Provider.Value1 = "btnF5 - Provider";

            F6_macro_Provider = new OpenSpan();
            F6_macro_Provider.Name1 = "macro_name";
            F6_macro_Provider.Value1 = "btnF6 - Provider";

            F7_macro_Provider = new OpenSpan();
            F7_macro_Provider.Name1 = "macro_name";
            F7_macro_Provider.Value1 = "btnF7 - Provider";

            F8_macro_Provider = new OpenSpan();
            F8_macro_Provider.Name1 = "macro_name";
            F8_macro_Provider.Value1 = "btnF8 - Provider";

            F9_macro_Provider = new OpenSpan();
            F9_macro_Provider.Name1 = "macro_name";
            F9_macro_Provider.Value1 = "btnF9 - Provider";

            F10_macro_Provider = new OpenSpan();
            F10_macro_Provider.Name1 = "macro_name";
            F10_macro_Provider.Value1 = "btnF10 - Provider";

            F11_macro_Provider = new OpenSpan();
            F11_macro_Provider.Name1 = "macro_name";
            F11_macro_Provider.Value1 = "btnF11 - Provider";

            F12_macro_Provider = new OpenSpan();
            F12_macro_Provider.Name1 = "macro_name";
            F12_macro_Provider.Value1 = "btnF12 - Provider";

            //PreCertification Macros
            F1_macro_PreCertification = new OpenSpan();
            F1_macro_PreCertification.Name1 = "macro_name";
            F1_macro_PreCertification.Value1 = "btnF1 - Pre-Certification";

            F2_macro_PreCertification = new OpenSpan();
            F2_macro_PreCertification.Name1 = "macro_name";
            F2_macro_PreCertification.Value1 = "btnF2 - Pre-Certification";

            F3_macro_PreCertification = new OpenSpan();
            F3_macro_PreCertification.Name1 = "macro_name";
            F3_macro_PreCertification.Value1 = "btnF3 - Pre-Certification";
            
            F4_macro_PreCertification = new OpenSpan();
            F4_macro_PreCertification.Name1 = "macro_name";
            F4_macro_PreCertification.Value1 = "btnF4 - Pre-Certification";

            F5_macro_PreCertification = new OpenSpan();
            F5_macro_PreCertification.Name1 = "macro_name";
            F5_macro_PreCertification.Value1 = "btnF5 - Pre-Certification";

            F6_macro_PreCertification = new OpenSpan();
            F6_macro_PreCertification.Name1 = "macro_name";
            F6_macro_PreCertification.Value1 = "btnF6 - Pre-Certification";

            F7_macro_PreCertification = new OpenSpan();
            F7_macro_PreCertification.Name1 = "macro_name";
            F7_macro_PreCertification.Value1 = "btnF7 - Pre-Certification";

            F8_macro_PreCertification = new OpenSpan();
            F8_macro_PreCertification.Name1 = "macro_name";
            F8_macro_PreCertification.Value1 = "btnF8 - Pre-Certification";

            F9_macro_PreCertification = new OpenSpan();
            F9_macro_PreCertification.Name1 = "macro_name";
            F9_macro_PreCertification.Value1 = "btnF9 - Pre-Certification";

            F10_macro_PreCertification = new OpenSpan();
            F10_macro_PreCertification.Name1 = "macro_name";
            F10_macro_PreCertification.Value1 = "btnF10 - Pre-Certification";

            F11_macro_PreCertification = new OpenSpan();
            F11_macro_PreCertification.Name1 = "macro_name";
            F11_macro_PreCertification.Value1 = "btnF11 - Pre-Certification";

            F12_macro_PreCertification = new OpenSpan();
            F12_macro_PreCertification.Name1 = "macro_name";
            F12_macro_PreCertification.Value1 = "btnF12 - Pre-Certification";

            //Medicaid Macros
            F1_macro_Medicaid = new OpenSpan();
            F1_macro_Medicaid.Name1 = "macro_name";
            F1_macro_Medicaid.Value1 = "btnF1 - Medicaid";

            F2_macro_Medicaid = new OpenSpan();
            F2_macro_Medicaid.Name1 = "macro_name";
            F2_macro_Medicaid.Value1 = "btnF2 - Medicaid";

            F3_macro_Medicaid = new OpenSpan();
            F3_macro_Medicaid.Name1 = "macro_name";
            F3_macro_Medicaid.Value1 = "btnF3 - Medicaid";

            F4_macro_Medicaid = new OpenSpan();
            F4_macro_Medicaid.Name1 = "macro_name";
            F4_macro_Medicaid.Value1 = "btnF4 - Medicaid";

            F5_macro_Medicaid = new OpenSpan();
            F5_macro_Medicaid.Name1 = "macro_name";
            F5_macro_Medicaid.Value1 = "btnF5 - Medicaid";

            F6_macro_Medicaid = new OpenSpan();
            F6_macro_Medicaid.Name1 = "macro_name";
            F6_macro_Medicaid.Value1 = "btnF6 - Medicaid";

            F7_macro_Medicaid = new OpenSpan();
            F7_macro_Medicaid.Name1 = "macro_name";
            F7_macro_Medicaid.Value1 = "btnF7 - Medicaid";

            F8_macro_Medicaid = new OpenSpan();
            F8_macro_Medicaid.Name1 = "macro_name";
            F8_macro_Medicaid.Value1 = "btnF8 - Medicaid";

            F9_macro_Medicaid = new OpenSpan();
            F9_macro_Medicaid.Name1 = "macro_name";
            F9_macro_Medicaid.Value1 = "btnF9 - Medicaid";

            F10_macro_Medicaid = new OpenSpan();
            F10_macro_Medicaid.Name1 = "macro_name";
            F10_macro_Medicaid.Value1 = "btnF10 - Medicaid";

            F11_macro_Medicaid = new OpenSpan();
            F11_macro_Medicaid.Name1 = "macro_name";
            F11_macro_Medicaid.Value1 = "btnF11 - Medicaid";

            F12_macro_Medicaid = new OpenSpan();
            F12_macro_Medicaid.Name1 = "macro_name";
            F12_macro_Medicaid.Value1 = "btnF12 - Medicaid";

            //Pricing Macros
            F1_macro_Pricing = new OpenSpan();
            F1_macro_Pricing.Name1 = "macro_name";
            F1_macro_Pricing.Value1 = "btnF1 - Pricing";

            F2_macro_Pricing = new OpenSpan();
            F2_macro_Pricing.Name1 = "macro_name";
            F2_macro_Pricing.Value1 = "btnF2 - Pricing";

            F3_macro_Pricing = new OpenSpan();
            F3_macro_Pricing.Name1 = "macro_name";
            F3_macro_Pricing.Value1 = "btnF3 - Pricing";

            F4_macro_Pricing = new OpenSpan();
            F4_macro_Pricing.Name1 = "macro_name";
            F4_macro_Pricing.Value1 = "btnF4 - Pricing";

            F5_macro_Pricing = new OpenSpan();
            F5_macro_Pricing.Name1 = "macro_name";
            F5_macro_Pricing.Value1 = "btnF5 - Pricing";

            F6_macro_Pricing = new OpenSpan();
            F6_macro_Pricing.Name1 = "macro_name";
            F6_macro_Pricing.Value1 = "btnF6 - Pricing";

            F7_macro_Pricing = new OpenSpan();
            F7_macro_Pricing.Name1 = "macro_name";
            F7_macro_Pricing.Value1 = "btnF7 - Pricing";

            F8_macro_Pricing = new OpenSpan();
            F8_macro_Pricing.Name1 = "macro_name";
            F8_macro_Pricing.Value1 = "btnF8 - Pricing";

            F9_macro_Pricing = new OpenSpan();
            F9_macro_Pricing.Name1 = "macro_name";
            F9_macro_Pricing.Value1 = "btnF9 - Pricing";

            F10_macro_Pricing = new OpenSpan();
            F10_macro_Pricing.Name1 = "macro_name";
            F10_macro_Pricing.Value1 = "btnF10 - Pricing";

            F11_macro_Pricing = new OpenSpan();
            F11_macro_Pricing.Name1 = "macro_name";
            F11_macro_Pricing.Value1 = "btnF11 - Pricing";

            F12_macro_Pricing = new OpenSpan();
            F12_macro_Pricing.Name1 = "macro_name";
            F12_macro_Pricing.Value1 = "btnF12 - Pricing";

            //Facility Macros
            F1_macro_Facilty = new OpenSpan();
            F1_macro_Facilty.Name1 = "macro_name";
            F1_macro_Facilty.Value1 = "btnF1 - Facilty Tools";

            F2_macro_Facilty = new OpenSpan();
            F2_macro_Facilty.Name1 = "macro_name";
            F2_macro_Facilty.Value1 = "btnF2 - Facilty Tools";

            F3_macro_Facilty = new OpenSpan();
            F3_macro_Facilty.Name1 = "macro_name";
            F3_macro_Facilty.Value1 = "btnF3 - Facilty Tools";

            F4_macro_Facilty = new OpenSpan();
            F4_macro_Facilty.Name1 = "macro_name";
            F4_macro_Facilty.Value1 = "btnF4 - Facilty Tools";

            F5_macro_Facilty = new OpenSpan();
            F5_macro_Facilty.Name1 = "macro_name";
            F5_macro_Facilty.Value1 = "btnF5 - Facilty Tools";

            F6_macro_Facilty = new OpenSpan();
            F6_macro_Facilty.Name1 = "macro_name Tools";
            F6_macro_Facilty.Value1 = "btnF6 - Facilty Tools";

            F7_macro_Facilty = new OpenSpan();
            F7_macro_Facilty.Name1 = "macro_name";
            F7_macro_Facilty.Value1 = "btnF7 - Facilty Tools";

            F8_macro_Facilty = new OpenSpan();
            F8_macro_Facilty.Name1 = "macro_name";
            F8_macro_Facilty.Value1 = "btnF8 - Facilty Tools";

            F9_macro_Facilty = new OpenSpan();
            F9_macro_Facilty.Name1 = "macro_name";
            F9_macro_Facilty.Value1 = "btnF9 - Facilty Tools";

            F10_macro_Facilty = new OpenSpan();
            F10_macro_Facilty.Name1 = "macro_name";
            F10_macro_Facilty.Value1 = "btnF10 - Facilty Tools";

            F11_macro_Facilty = new OpenSpan();
            F11_macro_Facilty.Name1 = "macro_name";
            F11_macro_Facilty.Value1 = "btnF11 - Facilty Tools";

            F12_macro_Facilty = new OpenSpan();
            F12_macro_Facilty.Name1 = "macro_name";
            F12_macro_Facilty.Value1 = "btnF12 - Facilty Tools";
            
        }

        public static void SetAllHotkeys()
        {
            F1_hotkey = new OpenSpan();
            F1_hotkey.Name1 = "key";
            F1_hotkey.Value1 = "F1";

            F2_hotkey = new OpenSpan();
            F2_hotkey.Name1  = "key";
            F2_hotkey.Value1 = "F2";

            F3_hotkey = new OpenSpan();
            F3_hotkey.Name1 = "key";
            F3_hotkey.Value1 = "F3";

            F4_hotkey = new OpenSpan();
            F4_hotkey.Name1 = "key";
            F4_hotkey.Value1 = "F4";

            F5_hotkey = new OpenSpan();
            F5_hotkey.Name1 = "key";
            F5_hotkey.Value1 = "F5";

            F6_hotkey = new OpenSpan();
            F6_hotkey.Name1 = "key";
            F6_hotkey.Value1 = "F6";

            F7_hotkey = new OpenSpan();
            F7_hotkey.Name1 = "key";
            F7_hotkey.Value1 = "F7";

            F8_hotkey = new OpenSpan();
            F8_hotkey.Name1 = "key";
            F8_hotkey.Value1 = "F8";

            F9_hotkey = new OpenSpan();
            F9_hotkey.Name1 = "key";
            F9_hotkey.Value1 = "F9";

            F10_hotkey = new OpenSpan();
            F10_hotkey.Name1 = "key";
            F10_hotkey.Value1 = "F10";

            F11_hotkey = new OpenSpan();
            F11_hotkey.Name1 = "key";
            F11_hotkey.Value1 = "F11";

            F12_hotkey = new OpenSpan();
            F12_hotkey.Name1 = "key";
            F12_hotkey.Value1 = "F12";

            O_hotkey = new OpenSpan();
            O_hotkey.Name1 = "key";
            O_hotkey.Value1 = "O";

            AltA_hotkey = new OpenSpan();
            AltA_hotkey.Name1 = "modifier";
            AltA_hotkey.Value1 = "Alt";
            AltA_hotkey.Name2 = "key";
            AltA_hotkey.Value2 = "A";

            AltC_hotkey = new OpenSpan();
            AltC_hotkey.Name1 = "modifier";
            AltC_hotkey.Value1 = "Alt";
            AltC_hotkey.Name2 = "key";
            AltC_hotkey.Value2 = "C";
            
            AltG_hotkey = new OpenSpan();
            AltG_hotkey.Name1 = "modifier";
            AltG_hotkey.Value1 = "Alt";
            AltG_hotkey.Name2 = "key";
            AltG_hotkey.Value2 = "G";

            AltH_hotkey = new OpenSpan();
            AltH_hotkey.Name1 = "modifier";
            AltH_hotkey.Value1 = "Alt";
            AltH_hotkey.Name2 = "key";
            AltH_hotkey.Value2 = "H";

            AltM_hotkey = new OpenSpan();
            AltM_hotkey.Name1 = "modifier";
            AltM_hotkey.Value1 = "Alt";
            AltM_hotkey.Name2 = "key";
            AltM_hotkey.Value2 = "M";

            AltI_hotkey = new OpenSpan();
            AltI_hotkey.Name1 = "modifier";
            AltI_hotkey.Value1 = "Alt";
            AltI_hotkey.Name2 = "key";
            AltI_hotkey.Value2 = "I";

            AltO_hotkey = new OpenSpan();
            AltO_hotkey.Name1 = "modifier";
            AltO_hotkey.Value1 = "Alt";
            AltO_hotkey.Name2 = "key";
            AltO_hotkey.Value2 = "O";

            AltP_hotkey = new OpenSpan();
            AltP_hotkey.Name1 = "modifier";
            AltP_hotkey.Value1 = "Alt";
            AltP_hotkey.Name2 = "key";
            AltP_hotkey.Value2 = "P";

            AltR_hotkey = new OpenSpan();
            AltR_hotkey.Name1 = "modifier";
            AltR_hotkey.Value1 = "Alt";
            AltR_hotkey.Name2 = "key";
            AltR_hotkey.Value2 = "R";

            CtrlM_hotkey = new OpenSpan();
            CtrlM_hotkey.Name1 = "modifier";
            CtrlM_hotkey.Value1 = "Control";
            CtrlM_hotkey.Name2 = "key";
            CtrlM_hotkey.Value2 = "M";

            ShiftF4_hotkey = new OpenSpan();
            ShiftF4_hotkey.Name1 = "modifier";
            ShiftF4_hotkey.Value1 = "Shift";
            ShiftF4_hotkey.Name2 = "key";
            ShiftF4_hotkey.Value2 = "F4";
            
            H_hotkey = new OpenSpan();
            H_hotkey.Name1 = "key";
            H_hotkey.Value1 = "H";

            I_hotkey = new OpenSpan();
            I_hotkey.Name1 = "key";
            I_hotkey.Value1 = "I";

            M_hotkey = new OpenSpan();
            M_hotkey.Name1 = "key";
            M_hotkey.Value1 = "M";

            Tab_hotkey = new OpenSpan();
            Tab_hotkey.Name1 = "key";
            Tab_hotkey.Value1 = "Tab";

            Enter_hotkey = new OpenSpan();
            Enter_hotkey.Name1 = "key";
            Enter_hotkey.Value1 = "Enter";

        }
        
    }
    
    public class OpenSpan
    {   
        public string TimeStamp { get; set; }
        public string Name1 { get; set; }
        public string Value1 { get; set; }
        public string Name2 { get; set; }
        public string Value2 { get; set; }
        public string Name3 { get; set; }
        public string Value3 { get; set; }

        public OpenSpan()
        {
            TimeStamp = "";
            Name1 = "";
            Value1 = "";
            Name2 = "";
            Value2 = "";
            Name3 = "";
            Value3 = "";
        }

    }

    public class OpenSpanClose
    {
        public string trigger { get; set; }
        public string document_id { get; set; }
        public string oploc { get; set; }
        public string pend_reason_code { get; set; }
        public string application_id { get; set; }
        public string work_unit_status_code { get; set; }


        public OpenSpanClose()
        {
            trigger = "";
            document_id = "";
            oploc = "";
            pend_reason_code = "";
            application_id = "";
            work_unit_status_code = "";

        }

    }
    
    public class FocusInStruct
    {
        public string ApplicationId { get; set; }
        public string TimeStamp { get; set; }

        public FocusInStruct()
        {
            ApplicationId = "";
            TimeStamp = "";
        }
    }


    public class OpenSpanWindows
    {

        public static void CloseOpenSpanWindow()
        {

            Console.WriteLine("Launch CloseOpenSpanWindow() procedure");
            Thread.Sleep(35000);


            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("Attempt " + i.ToString() + " of 5");
                System.Diagnostics.Process[] myProcess = System.Diagnostics.Process.GetProcesses();

                foreach (System.Diagnostics.Process p in myProcess)
                {
                    if (p.ProcessName.Contains("OpenSpan.Runtime"))
                    {
                        White.Core.Application OpenSpan = White.Core.Application.Attach("OpenSpan.Runtime");
                        Thread.Sleep(5000);

                        List<Window> OpenSpanWindows = OpenSpan.GetWindows();
                        foreach (Window OpenSpanWin in OpenSpanWindows)
                        {
                            Console.WriteLine("Close OpenSpan Window");
                            Console.WriteLine("Openspan Window: " + OpenSpanWin.Name);

                            if (OpenSpanWin.Name.Contains("OpenSpan Project"))
                            {
                                OpenSpanWin.Close();
                                return;
                            }

                        }
                    }
                }
                Thread.Sleep(3000);
            }
        }


        public static void WaitOpenSpan(int delay)
        {

            Console.WriteLine("Launch WaitOpenSpan(int delay) procedure");
            Thread.Sleep(delay);

        }

        public static void WaitOpenSpan()
        {

            Console.WriteLine("Launch WaitOpenSpan() procedure");
            Thread.Sleep(35000);


            for (int i = 1; i < 5; i++)
            {
                Console.WriteLine("Attempt " + i.ToString() + " of 5");
                System.Diagnostics.Process[] myProcess = System.Diagnostics.Process.GetProcesses();

                foreach (System.Diagnostics.Process p in myProcess)
                {
                    if (p.ProcessName.Contains("OpenSpan.Runtime"))
                    {
                        White.Core.Application OpenSpan = White.Core.Application.Attach("OpenSpan.Runtime");
                        Thread.Sleep(5000);

                        var OpenSpanPanels = Desktop.Instance.GetMultiple(SearchCriteria.ByControlType(ControlType.Pane));

                        foreach (var OpenSpanPanel in OpenSpanPanels)
                        {
                            Console.WriteLine("Search OpenSpan application Bar");

                            if (OpenSpanPanel.Name.Contains("Application Bar"))
                            {
                                return;
                            }

                        }
                    }
                }
                Thread.Sleep(3000);
            }
        }

    }
      
}
