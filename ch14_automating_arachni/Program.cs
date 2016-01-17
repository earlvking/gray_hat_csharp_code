﻿using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace ch14_automating_arachni
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (ArachniSession session = new ArachniSession ("127.0.0.1", 7331)) {
				using (ArachniManager manager = new ArachniManager (session)) {
					JObject scanId = manager.StartScan ("http://demo.testfire.net/default.aspx");
					Guid id = Guid.Parse(scanId ["id"].ToString ());
					JObject scan = manager.GetScanStatus (id);

					while (scan ["status"].ToString() != "done") {
						Console.WriteLine ("Sleeping");
						System.Threading.Thread.Sleep (100);
						scan = manager.GetScanStatus (id);
					}

					Console.WriteLine (scan.ToString ());
				}
			}
		}
	}
}
