using Microsoft.AspNetCore.Mvc;

namespace Sanatorium.Controllers
{
    public class QueryController : Controller
    {
        private readonly sanatoriumContext _db;
        public QueryController(sanatoriumContext db)
        {
            _db = db;
        }
        public ActionResult Index() { return View(); }


        public IActionResult GetAlcoholicsInspectors(QueryModel model)
        {
          var inspectors = _db.AlcoholicInspectors
          .Where(e => e.AlcoholicId == model.AlcoholicId && e.State == 1 && e.Date >= model.From && e.Date <= model.To)
          .Select(e => e.Inspector.User) 
          .Distinct()
          .ToList();

            return View(inspectors);
        }

        public IActionResult GetAlcoholicsBeds(QueryModel model)
        {
            var bedIds = _db.AlcoholicInspectors.Where(e => e.AlcoholicId == model.AlcoholicId && e.Date >= model.From && e.Date <= model.To).Select(e => e.BedId).ToList();

            var beds = new List<Bed>();
            foreach (var bedID in bedIds)
            {
                var bed = _db.Beds.FirstOrDefault(e => e.Id == bedID);
                if (bed != null)
                {
                    beds.Add(bed);
                }
            }

            return View(beds); ;
        }

        public IActionResult GetAlcoholicsForInspectorTimesPutInBed(QueryModel model)
        {
            var alcoholics = _db.People
                    .Where(person =>
                        _db.AlcoholicInspectors
                            .Count(ai => ai.InspectorId == model.InspectorId &&
                                         ai.Date >= model.From &&
                                         ai.Date <= model.To &&
                                         ai.State == 1 &&
                                         ai.AlcoholicId == person.Id) >= model.Times)
                    .ToList();

            return View(alcoholics);
        }

        public IActionResult GetAlcoholicsEscapedBeds(QueryModel model)
        {
            var escapedAlcoholicsBeds = _db.AlcoholicInspectors.Where(e => e.AlcoholicId == model.AlcoholicId && e.Date >= model.From && e.Date <= model.To).ToList();
            var beds = new List<Bed>();
            foreach(var bed in escapedAlcoholicsBeds)
            {
                beds.Add(_db.Beds.FirstOrDefault(e => e.Id == bed.BedId));
            }
            beds = beds.DistinctBy(e =>  e.Id).ToList();
            return View(beds);
        }

       

        public IActionResult GetAlcoholicsTimesPutInBed(QueryModel model)
        {
            var alcoholicsPutInBed = _db.AlcoholicInspectors
        .Where(e => e.Date >= model.From && e.Date <= model.To && e.State == 1)
        .ToList();

            var alcoholics = alcoholicsPutInBed
                .GroupBy(e => e.AlcoholicId)
                .Where(group => group.Count() >= model.Times)
                .Select(group => _db.People.FirstOrDefault(person => person.Id == group.Key))
                .Where(person => person != null)
                .DistinctBy(person => person.Id)
                .ToList();

            return View(alcoholics);
        }

        public IActionResult GetAllPairsForInspectorAndAlcoholic(QueryModel model)
        {
            var alcoholicsInspectors = _db.AlcoholicInspectors
    .Where(e => e.Date >= model.From && e.Date <= model.To && e.InspectorId == model.InspectorId && e.AlcoholicId == model.AlcoholicId)
    .ToList();

            return View(alcoholicsInspectors);
        }

        public IActionResult EscapedTotalByMonth()
        {
            var date = DateTime.Now;
            var escapedInJanuary = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/01/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/02/01").AddDays(-1)).Count();
            var escapedInFebruary = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/02/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/03/01").AddDays(-1)).Count();
            var escapedInMarch = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/03/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/04/01").AddDays(-1)).Count();
            var escapedInApril = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/04/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/05/01").AddDays(-1)).Count();
            var escapedInMay = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/05/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/06/01").AddDays(-1)).Count();
            var escapedInJune = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/06/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/07/01").AddDays(-1)).Count();
            var escapedInJuly = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/07/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/08/01").AddDays(-1)).Count();
            var escapedInAugust = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/08/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/09/01").AddDays(-1)).Count();
            var escapedInSeptember = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/09/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/10/01").AddDays(-1)).Count();
            var escapedInOctober = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/10/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/11/01").AddDays(-1)).Count();
            var escapedInNovember = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/11/01") && e.State == 3 && e.Date <= DateTime.Parse("2023/12/01").AddDays(-1)).Count();
            var escapedInDecember = _db.AlcoholicInspectors.Where(e => e.Date >= DateTime.Parse("2023/12/01") && e.State == 3 && e.Date <= DateTime.Parse("2024/01/01").AddDays(-1)).Count();

            return View(new List<(string, int)> { new("January", escapedInJanuary), new ("February", escapedInFebruary),
                new ("March", escapedInMarch), new ("April", escapedInApril), new ("May", escapedInMay), new ("June", escapedInJune),
                new ("July", escapedInJuly), new ("August", escapedInAugust), new ("September", escapedInSeptember), new ("October", escapedInOctober),
                new ("November", escapedInNovember), new ("December", escapedInDecember)});
        }

        public IActionResult GetInspectorsReleasedLessThenPutToBed(QueryModel model)
        {
            var alcoholicsInspectorsThatPutHimInBed = _db.AlcoholicInspectors
          .Where(e => e.AlcoholicId == model.AlcoholicId && e.State == 1 && e.Date >= model.From && e.Date <= model.To);

            var alcoholicsInspectorsThatReleasedOrEscaped = _db.AlcoholicInspectors
                .Where(e => e.AlcoholicId == model.AlcoholicId && (e.State == 2 || e.State == 3) && e.Date >= model.From && e.Date <= model.To);

            var inspectorsPutIds = alcoholicsInspectorsThatPutHimInBed.Select(e => e.InspectorId).Distinct().ToList();
            var inspectorsReleasedOrEscapedIds = alcoholicsInspectorsThatReleasedOrEscaped.Select(e => e.InspectorId).Distinct().ToList();

            var inspectorsIds = inspectorsPutIds
                .Where(inspectorId =>
                    alcoholicsInspectorsThatPutHimInBed.Count(e => e.InspectorId == inspectorId) >
                    alcoholicsInspectorsThatReleasedOrEscaped.Count(e => e.InspectorId == inspectorId))
                .Union(inspectorsReleasedOrEscapedIds)
                .ToList();

            var inspectors = _db.People
                .Where(p => inspectorsIds.Contains(p.Id))
                .ToList();

            return View(inspectors);

            


        }
    }
}
