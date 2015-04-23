using System;

namespace SURGE.Common
{
	public class Types
	{
		public Types ()
		{
		}

		public class Job{
			public int ID { get; set;}
			public string Title{get;set;}
			public string JobDesc {get;set;}
			public DateTime JobStartDate {get;set;}
			public string JobStartTime {get;set;}
			public string JobEndTime{get;set;}
			public float Budget{ get; set;}
			public bool ForHospital{get;set;}
			public string Status{get;set;}
			public int CreatorId{get;set;}
			public string CreatorType{ get; set;}
		}

	}
}

