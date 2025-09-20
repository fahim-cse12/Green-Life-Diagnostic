namespace Shared.Utility
{
    public static class DatabaseProcedure
    {
        public static readonly string PatientSearchByQuery = "SP_SearchPatient";
        public static readonly string TicketDeleteQuery = "SP_DeletePurchasedTicket";
        public static readonly string AppointmentWiseIncomeQuery = "Sp_AppointmentWiseIncome";
        public static readonly string TestWiseIncomeReportQuery = "Sp_TestWiseIncomeReport";
        public static readonly string DashboardQuery = "SP_GetDashboardData";
    }
}
