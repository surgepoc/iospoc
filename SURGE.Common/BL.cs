using System;
using System.Data;
using System.Xml;

namespace SURGE.Common
{
	public class BL
	{
		public BL ()
		{
		}

		//To post new job
		public static DataTable PostJob(Types.Job newJob){
			return DL.PostJob (newJob);
		}

		//To get specific job details
		public static DataTable GetjobDetail(int jobId){
			return DL.GetJobDetails (jobId);
		}

		//To get provider details to tag for job by job creator
		public static DataTable GetProvidersToTag(){
			return DL.GetProvidersToTag ();
		}

		//To tag a provider for a job
		public static bool TagProviderForJob(int jobId, int providerId){
			return DL.TagProviderForJob (jobId, providerId);
		}

		//To get provider details who are tagged for a job
		public static DataTable GetProvidersTaggedForJob(int jobId){
			return DL.GetProvidersTaggedForJob (jobId);
		}

		//To get providers list who are interested in a job
		public static DataTable GetProvidersInterestedInJob(int jobId){
			return DL.GetProvidersInterestedInJob (jobId);
		}

		//To get provider details based on job id
		public static DataTable GetProviderDetailsByJob(int jobId, int providerId){
			return DL.GetProviderDetailsByJob (jobId, providerId);
		}

		//To award a job to a provider
		public static bool AwardJob(int jobId, int providerId){
			return DL.AwardJob (jobId, providerId);
		}

		//To change status of a job
		public static bool ChangeJobStatus(int jobId, string jobStatus){
			return DL.ChangeJobStatus (jobId, jobStatus);
		}

		//To tag a job by provider
		public static bool BidTaskByProvider(int jobId, int providerId, int bidAmount){
			return DL.BidTaskByProvider (jobId, providerId, bidAmount);
		}

		//To get all jobs for Admin/Hospitalist
		public static DataTable GetAllJobsForAdmins(int adminId, int hospitalistId, int staffId){
			return DL.GetAllJobsForAdmins (adminId, hospitalistId, staffId);
		}
	}
}

