int days = 3;    // tu dac dni
Doctor d = null; // tu dac doktora
bool freeonly = false // tu dac freeonly
DateTime dt = DateTime.Today;
DateTime dt1 = DateTime.Today;
dt1 = dt1.AddHours(DateTime.Now.Hour);
dt1 = dt1.AddMinutes(DateTime.Now.Minute < 15 ? 15 : DateTime.Now.Minute < 30 ? 30 : DateTime.Now.Minute < 45 ? 45 : 60);
var returnList = new List<VisitTime>();
for(int i = 0; i < days; i++)
{
	dt = dt.AddHours(d.StartingHour);
	dt = dt1 > dt ? dt1 : dt;
	dt1 = DateTime.Today;

	while(true)
	{
		returnList.Add(new VisitTime(d, dt, -1));
		dt = dt.AddMinutes(15);
		if (dt.Hour >= d.EndingHour)
			break;
							
	}
	dt = dt.Date.AddDays(1);
	if (dt.DayOfWeek == DayOfWeek.Saturday) dt = dt.AddDays(2);
}  
using (IVisitClient vc = CF.GetVisitClient())
{
	var list = (await vc.GetDoctorVisits(d.LicenseNumber)).ToList(); // tu zassac wizyty doktora
	foreach(var v in list)
	{
		returnList.ForEach(visit => { if (visit.DT == v.DT) visit.Patient = v.Patient; }));
	}
	if (freeonly) returnList.RemoveAll(visit => visit.Patient != null));
}
return returnList;