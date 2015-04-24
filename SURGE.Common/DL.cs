using System;
using System.Data;
using System.Data.SqlClient;
using Mono.Data.Sqlite;

namespace SURGE.Common
{
	public class DL
	{
		private static string _cs = 
			"Data Source = madhurams.co.in; Initial Catalog = surgepoc; user id = surgepoc; password = Sairam123%";

		public DL ()
		{
		}

		//To create new job
		public static DataTable PostJob(Types.Job newJob){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand ("SP_GetSetJobs", myCon);
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@title", newJob.Title);
			myCmd.Parameters.AddWithValue ("@jobDesc", newJob.JobDesc);
			myCmd.Parameters.AddWithValue ("@jobStartDate", newJob.JobStartDate);
			myCmd.Parameters.AddWithValue ("@jobStartTime", newJob.JobStartTime);
			myCmd.Parameters.AddWithValue ("@jobEndTime", newJob.JobEndTime);
			myCmd.Parameters.AddWithValue ("@budget", newJob.Budget);
			myCmd.Parameters.AddWithValue ("@forHospital", newJob.ForHospital);
			myCmd.Parameters.AddWithValue ("@creatorId", newJob.CreatorId);
			myCmd.Parameters.AddWithValue ("@creatorType", newJob.CreatorType);
			myCmd.Parameters.AddWithValue ("@ptype", 1);

			SqlDataAdapter myAdp = new SqlDataAdapter (myCmd);
			DataTable dtJob = new DataTable ();
			myAdp.Fill (dtJob);
			return dtJob;
		}

		//To get specific job details
		public static DataTable GetJobDetails(int jobId){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand ("SP_GetSetJobs", myCon);
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@id", jobId);
			myCmd.Parameters.AddWithValue ("@ptype", 3);

			SqlDataAdapter myAdp = new SqlDataAdapter (myCmd);
			DataTable dtJob = new DataTable ();
			myAdp.Fill (dtJob);
			return dtJob;
		}

		//To get provider details to tag for job by job creator
		public static DataTable GetProvidersToTag(){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand ("SP_GetSetTagProviders", myCon);
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@ptype", 3);

			SqlDataAdapter myAdp = new SqlDataAdapter (myCmd);
			DataTable dtStaff = new DataTable ();
			myAdp.Fill (dtStaff);
			return dtStaff;
		}

		//To tag a provider for a job
		public static bool TagProviderForJob(int jobId, int providerId){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand ("SP_GetSetTagProviders", myCon);
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@jobId", jobId);
			myCmd.Parameters.AddWithValue ("@providerId", providerId);
			myCmd.Parameters.AddWithValue ("@ptype", 1);

			try{
				myCon.Open();
				myCmd.ExecuteNonQuery();
			}
			catch{
				return false;
			}
			finally{
				myCon.Close();
			}
			return true;
		}

		//To get providers who are tagged to a job by creator
		public static DataTable GetProvidersTaggedForJob(int jobId){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand ("SP_GetSetTagProviders", myCon);
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@jobId", jobId);
			myCmd.Parameters.AddWithValue ("@ptype", 2);

			SqlDataAdapter myAdp = new SqlDataAdapter (myCmd);
			DataTable dtProviders = new DataTable ();
			myAdp.Fill (dtProviders);

			return dtProviders;
		}

		//To get provider details who showed interest in a job
		public static DataTable GetProvidersInterestedInJob(int jobId){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand ("SP_GetSetTagJobs", myCon);
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@jobId", jobId);
			myCmd.Parameters.AddWithValue ("@ptype", 3);

			SqlDataAdapter myAdp = new SqlDataAdapter (myCmd);
			DataTable dtStaff = new DataTable ();
			myAdp.Fill (dtStaff);
			return dtStaff;
		}

		//To get details of provider who bid for a specific job
		public static DataTable GetProviderDetailsByJob(int jobId, int providerId){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand ("SP_GetSetTagJobs", myCon);
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@jobId", jobId);
			myCmd.Parameters.AddWithValue ("@providerId", providerId);
			myCmd.Parameters.AddWithValue ("@ptype", 4);

			SqlDataAdapter myAdp = new SqlDataAdapter (myCmd);
			DataTable dtProvider = new DataTable ();
			myAdp.Fill (dtProvider);
			return dtProvider;
		}

		//To award a job to a provider
		public static bool AwardJob(int jobId, int providerId){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand ("SP_GetSetTagJobs", myCon);
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@jobId", jobId);
			myCmd.Parameters.AddWithValue ("@providerId", providerId);
			myCmd.Parameters.AddWithValue ("@ptype", 2);

			try{
				myCon.Open();
				myCmd.ExecuteNonQuery();
			}
			catch{
				return false;
			}
			finally{
				myCon.Close();
			}
			return true;
		}

		//To change status of a job
		public static bool ChangeJobStatus(int jobId, string jobStatus ){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand("SP_GetSetTagJobs", myCon); 
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@jobId", jobId);
			myCmd.Parameters.AddWithValue ("@jobStatus", jobStatus);
			myCmd.Parameters.AddWithValue ("@ptype", 5);

			try{
				myCon.Open();
				myCmd.ExecuteNonQuery();
			}
			catch{
				return false;
			}
			finally{
				myCon.Close();
			}
			return true;
		}

		//To bid task by provider 
		public static bool BidTaskByProvider(int jobId, int providerId, int bidAmount){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand("SP_GetSetTagJobs", myCon); 
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@jobId", jobId);
			myCmd.Parameters.AddWithValue ("@providerId", providerId);
			myCmd.Parameters.AddWithValue ("@bidAmount", bidAmount);
			myCmd.Parameters.AddWithValue ("@ptype", 1);

			try{
				myCon.Open();
				myCmd.ExecuteNonQuery();
			}
			catch{
				return false;
			}
			finally{
				myCon.Close();
			}
			return true;
		}
			
		//To get all jobs for Admin/Hospitalist
		public static DataTable GetAllJobsForAdmins(int adminId, int hospitalistId, int staffId){
			SqlConnection myCon = new SqlConnection (_cs);

			SqlCommand myCmd = new SqlCommand("SP_GetSetJobs", myCon); 
			myCmd.CommandType = CommandType.StoredProcedure;
			myCmd.Parameters.AddWithValue ("@adminId", adminId);
			myCmd.Parameters.AddWithValue ("@hospitalistId", hospitalistId);
			myCmd.Parameters.AddWithValue ("@staffId", staffId);
			myCmd.Parameters.AddWithValue ("@ptype", 4);

			SqlDataAdapter myAdp = new SqlDataAdapter (myCmd);
			DataTable dtJobs = new DataTable ();
			myAdp.Fill (dtJobs);

			return dtJobs;
		}
	}
}

