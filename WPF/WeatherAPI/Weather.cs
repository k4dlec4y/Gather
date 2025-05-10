using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using WPF.Models;

namespace WPF.WeatherAPI;

public static class WeatherFetcher
{
	public static async Task<string> SetWeather(Event e)
	{
		try
		{
			return await GetWeatherAsync(e.Location.Trim(), e.Date);
		}
		catch (Exception ex)
		{
			return $"Error: {ex.Message}";
		}
	}

	private static async Task<string> GetWeatherAsync(string location, DateTime selectedDate)
	{
		using HttpClient client = new HttpClient();
		string url = $"https://wttr.in/{location}?format=j1";

		HttpResponseMessage response = await client.GetAsync(url);
		if (response.StatusCode == HttpStatusCode.NotFound)
		{
			return "This location doesn't exist or the organizer made a typo in location name";
		}
		response.EnsureSuccessStatusCode();

		string jsonResponse = await response.Content.ReadAsStringAsync();
		var weatherData = JsonConvert.DeserializeObject<Temp>(jsonResponse);
		if (weatherData == null)
		{
			return "No weather data available for the selected date or location.";
		}

		int dayOffset = (selectedDate.Date - DateTime.Today).Days;
		if (dayOffset > 2)
		{
			return "It is too early to forecast weather.";
		}

		var selectedWeather = weatherData.Weather.FirstOrDefault(w => w.Date.Date == selectedDate.Date);
		if (selectedWeather == null)
		{
			return "No weather data available for the selected date or location.";
		}

		var hourlyDetails = selectedWeather.Hourly
			.Select(h =>
			{
				int hour = int.Parse(h.Time) / 100;
				return $"{hour}:00 - {h.TempC}°C, {h.WeatherDesc[0].Value}, Wind: {h.WindspeedKmph} km/h";
			});

		return string.Join("\n", hourlyDetails);
	}
}

public partial class Temp
{
	[JsonProperty("current_condition")]
	public CurrentCondition[] CurrentCondition { get; set; }

	[JsonProperty("nearest_area")]
	public NearestArea[] NearestArea { get; set; }

	[JsonProperty("request")]
	public Request[] Request { get; set; }

	[JsonProperty("weather")]
	public Weather[] Weather { get; set; }
}

public partial class CurrentCondition
{
	[JsonProperty("FeelsLikeC")]
	public string FeelsLikeC { get; set; }

	[JsonProperty("FeelsLikeF")]
	public string FeelsLikeF { get; set; }

	[JsonProperty("cloudcover")]
	public string Cloudcover { get; set; }

	[JsonProperty("humidity")]
	public string Humidity { get; set; }

	[JsonProperty("localObsDateTime")]
	public string LocalObsDateTime { get; set; }

	[JsonProperty("observation_time")]
	public string ObservationTime { get; set; }

	[JsonProperty("precipInches")]
	public string PrecipInches { get; set; }

	[JsonProperty("precipMM")]
	public string PrecipMm { get; set; }

	[JsonProperty("pressure")]
	public string Pressure { get; set; }

	[JsonProperty("pressureInches")]
	public string PressureInches { get; set; }

	[JsonProperty("temp_C")]
	public string TempC { get; set; }

	[JsonProperty("temp_F")]
	public string TempF { get; set; }

	[JsonProperty("uvIndex")]
	public string UvIndex { get; set; }

	[JsonProperty("visibility")]
	public string Visibility { get; set; }

	[JsonProperty("visibilityMiles")]
	public string VisibilityMiles { get; set; }

	[JsonProperty("weatherCode")]
	public string WeatherCode { get; set; }

	[JsonProperty("weatherDesc")]
	public WeatherDesc[] WeatherDesc { get; set; }

	[JsonProperty("weatherIconUrl")]
	public WeatherDesc[] WeatherIconUrl { get; set; }

	[JsonProperty("winddir16Point")]
	public string Winddir16Point { get; set; }

	[JsonProperty("winddirDegree")]
	public string WinddirDegree { get; set; }

	[JsonProperty("windspeedKmph")]
	public string WindspeedKmph { get; set; }

	[JsonProperty("windspeedMiles")]
	public string WindspeedMiles { get; set; }
}

public partial class WeatherDesc
{
	[JsonProperty("value")]
	public string Value { get; set; }
}

public partial class NearestArea
{
	[JsonProperty("areaName")]
	public WeatherDesc[] AreaName { get; set; }

	[JsonProperty("country")]
	public WeatherDesc[] Country { get; set; }

	[JsonProperty("latitude")]
	public string Latitude { get; set; }

	[JsonProperty("longitude")]
	public string Longitude { get; set; }

	[JsonProperty("population")]
	public string Population { get; set; }

	[JsonProperty("region")]
	public WeatherDesc[] Region { get; set; }

	[JsonProperty("weatherUrl")]
	public WeatherDesc[] WeatherUrl { get; set; }
}

public partial class Request
{
	[JsonProperty("query")]
	public string Query { get; set; }

	[JsonProperty("type")]
	public string Type { get; set; }
}

public partial class Weather
{
	[JsonProperty("astronomy")]
	public Astronomy[] Astronomy { get; set; }

	[JsonProperty("avgtempC")]
	public string AvgtempC { get; set; }

	[JsonProperty("avgtempF")]
	public string AvgtempF { get; set; }

	[JsonProperty("date")]
	public DateTimeOffset Date { get; set; }

	[JsonProperty("hourly")]
	public Hourly[] Hourly { get; set; }

	[JsonProperty("maxtempC")]
	public string MaxtempC { get; set; }

	[JsonProperty("maxtempF")]
	public string MaxtempF { get; set; }

	[JsonProperty("mintempC")]
	public string MintempC { get; set; }

	[JsonProperty("mintempF")]
	public string MintempF { get; set; }

	[JsonProperty("sunHour")]
	public string SunHour { get; set; }

	[JsonProperty("totalSnow_cm")]
	public string TotalSnowCm { get; set; }

	[JsonProperty("uvIndex")]
	public string UvIndex { get; set; }
}

public partial class Astronomy
{
	[JsonProperty("moon_illumination")]
	public string MoonIllumination { get; set; }

	[JsonProperty("moon_phase")]
	public string MoonPhase { get; set; }

	[JsonProperty("moonrise")]
	public string Moonrise { get; set; }

	[JsonProperty("moonset")]
	public string Moonset { get; set; }

	[JsonProperty("sunrise")]
	public string Sunrise { get; set; }

	[JsonProperty("sunset")]
	public string Sunset { get; set; }
}

public partial class Hourly
{
	[JsonProperty("DewPointC")]
	public string DewPointC { get; set; }

	[JsonProperty("DewPointF")]
	public string DewPointF { get; set; }

	[JsonProperty("FeelsLikeC")]
	public string FeelsLikeC { get; set; }

	[JsonProperty("FeelsLikeF")]
	public string FeelsLikeF { get; set; }

	[JsonProperty("HeatIndexC")]
	public string HeatIndexC { get; set; }

	[JsonProperty("HeatIndexF")]
	public string HeatIndexF { get; set; }

	[JsonProperty("WindChillC")]
	public string WindChillC { get; set; }

	[JsonProperty("WindChillF")]
	public string WindChillF { get; set; }

	[JsonProperty("WindGustKmph")]
	public string WindGustKmph { get; set; }

	[JsonProperty("WindGustMiles")]
	public string WindGustMiles { get; set; }

	[JsonProperty("chanceoffog")]
	public string Chanceoffog { get; set; }

	[JsonProperty("chanceoffrost")]
	public string Chanceoffrost { get; set; }

	[JsonProperty("chanceofhightemp")]
	public string Chanceofhightemp { get; set; }

	[JsonProperty("chanceofovercast")]
	public string Chanceofovercast { get; set; }

	[JsonProperty("chanceofrain")]
	public string Chanceofrain { get; set; }

	[JsonProperty("chanceofremdry")]
	public string Chanceofremdry { get; set; }

	[JsonProperty("chanceofsnow")]
	public string Chanceofsnow { get; set; }

	[JsonProperty("chanceofsunshine")]
	public string Chanceofsunshine { get; set; }

	[JsonProperty("chanceofthunder")]
	public string Chanceofthunder { get; set; }

	[JsonProperty("chanceofwindy")]
	public string Chanceofwindy { get; set; }

	[JsonProperty("cloudcover")]
	public string Cloudcover { get; set; }

	[JsonProperty("diffRad")]
	public string DiffRad { get; set; }

	[JsonProperty("humidity")]
	public string Humidity { get; set; }

	[JsonProperty("precipInches")]
	public string PrecipInches { get; set; }

	[JsonProperty("precipMM")]
	public string PrecipMm { get; set; }

	[JsonProperty("pressure")]
	public string Pressure { get; set; }

	[JsonProperty("pressureInches")]
	public string PressureInches { get; set; }

	[JsonProperty("shortRad")]
	public string ShortRad { get; set; }

	[JsonProperty("tempC")]
	public string TempC { get; set; }

	[JsonProperty("tempF")]
	public string TempF { get; set; }

	[JsonProperty("time")]
	public string Time { get; set; }

	[JsonProperty("uvIndex")]
	public string UvIndex { get; set; }

	[JsonProperty("visibility")]
	public string Visibility { get; set; }

	[JsonProperty("visibilityMiles")]
	public string VisibilityMiles { get; set; }

	[JsonProperty("weatherCode")]
	public string WeatherCode { get; set; }

	[JsonProperty("weatherDesc")]
	public WeatherDesc[] WeatherDesc { get; set; }

	[JsonProperty("weatherIconUrl")]
	public WeatherDesc[] WeatherIconUrl { get; set; }

	[JsonProperty("winddir16Point")]
	public string Winddir16Point { get; set; }

	[JsonProperty("winddirDegree")]
	public string WinddirDegree { get; set; }

	[JsonProperty("windspeedKmph")]
	public string WindspeedKmph { get; set; }

	[JsonProperty("windspeedMiles")]
	public string WindspeedMiles { get; set; }
}