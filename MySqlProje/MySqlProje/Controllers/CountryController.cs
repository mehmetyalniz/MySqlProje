using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlProje.Models;
using MySqlProje.Services;

namespace MySqlProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public CountryController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // Tüm ülkeleri getiren method
        [HttpGet]
        public ActionResult GetAllCountry()
        {
            var countries = _databaseService.GetAllCountrys();
            return Ok(countries);
        }

        // Ülke ekleme işlemi sadece admin yetkisine sahip kullanıcılar tarafından yapılabilir
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AddCountry([FromBody] CountrY country)
        {
            if (country == null)
            {
                return BadRequest("Ülke bilgileri eksik.");
            }

            _databaseService.AddCountry(country);
            return Ok("Ülke başarıyla eklendi");
        }

        // Ülke güncelleme işlemi hem admin hem user tarafından yapılabilir
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,user")]
        public IActionResult UpdateCountry(int id, [FromBody] CountrY country)
        {
            if (country == null || country.CountryId != id)
            {
                return BadRequest("Ülke bilgileri eksik veya ID uyuşmuyor.");
            }

            var updated = _databaseService.UpdateCountry(id, country);
            if (updated)
            {
                return Ok("Ülke başarıyla güncellendi.");
            }
            return NotFound("Ülke bulunamadı.");
        }

        // Ülke silme işlemi sadece admin yetkisine sahip kullanıcılar tarafından yapılabilir
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteCountry(int id)
        {
            var deleted = _databaseService.DeleteCountry(id);
            if (deleted)
            {
                return Ok("Ülke başarıyla silindi.");
            }
            return NotFound("Ülke bulunamadı.");
        }
    }
}
