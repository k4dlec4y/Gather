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
	public required CurrentCondition[] CurrentCondition { get; set; }

	[JsonProperty("nearest_area")]
	public required NearestArea[] NearestArea { get; set; }

	[JsonProperty("request")]
	public required Request[] Request { get; set; }

	[JsonProperty("weather")]
	public required Weather[] Weather { get; set; }
}

public partial class CurrentCondition
{
	[JsonProperty("FeelsLikeC")]
	public required string FeelsLikeC { get; set; }

	[JsonProperty("FeelsLikeF")]
	public required string FeelsLikeF { get; set; }

	[JsonProperty("cloudcover")]
	public required string Cloudcover { get; set; }

	[JsonProperty("humidity")]
	public required string Humidity { get; set; }

	[JsonProperty("localObsDateTime")]
	public required string LocalObsDateTime { get; set; }

	[JsonProperty("observation_time")]
	public required string ObservationTime { get; set; }

	[JsonProperty("precipInches")]
	public required string PrecipInches { get; set; }

	[JsonProperty("precipMM")]
	public required string PrecipMm { get; set; }

	[JsonProperty("pressure")]
	public required string Pressure { get; set; }

	[JsonProperty("pressureInches")]
	public required string PressureInches { get; set; }

	[JsonProperty("temp_C")]
	public required string TempC { get; set; }

	[JsonProperty("temp_F")]
	public required string TempF { get; set; }

	[JsonProperty("uvIndex")]
	public required string UvIndex { get; set; }

	[JsonProperty("visibility")]
	public required string Visibility { get; set; }

	[JsonProperty("visibilityMiles")]
	public required string VisibilityMiles { get; set; }

	[JsonProperty("weatherCode")]
	public required string WeatherCode { get; set; }

	[JsonProperty("weatherDesc")]
	public required WeatherDesc[] WeatherDesc { get; set; }

	[JsonProperty("weatherIconUrl")]
	public required WeatherDesc[] WeatherIconUrl { get; set; }

	[JsonProperty("winddir16Point")]
	public required string Winddir16Point { get; set; }

	[JsonProperty("winddirDegree")]
	public required string WinddirDegree { get; set; }

	[JsonProperty("windspeedKmph")]
	public required string WindspeedKmph { get; set; }

	[JsonProperty("windspeedMiles")]
	public required string WindspeedMiles { get; set; }
}

public partial class WeatherDesc
{
	[JsonProperty("value")]
	public required string Value { get; set; }
}

public partial class NearestArea
{
	[JsonProperty("areaName")]
	public required WeatherDesc[] AreaName { get; set; }

	[JsonProperty("country")]
	public required WeatherDesc[] Country { get; set; }

	[JsonProperty("latitude")]
	public required string Latitude { get; set; }

	[JsonProperty("longitude")]
	public required string Longitude { get; set; }

	[JsonProperty("population")]
	public required string Population { get; set; }

	[JsonProperty("region")]
	public required WeatherDesc[] Region { get; set; }

	[JsonProperty("weatherUrl")]
	public required WeatherDesc[] WeatherUrl { get; set; }
}

public partial class Request
{
	[JsonProperty("query")]
	public required string Query { get; set; }

	[JsonProperty("type")]
	public required string Type { get; set; }
}

public partial class Weather
{
	[JsonProperty("astronomy")]
	public required Astronomy[] Astronomy { get; set; }

	[JsonProperty("avgtempC")]
	public required string AvgtempC { get; set; }

	[JsonProperty("avgtempF")]
	public required string AvgtempF { get; set; }

	[JsonProperty("date")]
	public required DateTimeOffset Date { get; set; }

	[JsonProperty("hourly")]
	public required Hourly[] Hourly { get; set; }

	[JsonProperty("maxtempC")]
	public required string MaxtempC { get; set; }

	[JsonProperty("maxtempF")]
	public required string MaxtempF { get; set; }

	[JsonProperty("mintempC")]
	public required string MintempC { get; set; }

	[JsonProperty("mintempF")]
	public required string MintempF { get; set; }

	[JsonProperty("sunHour")]
	public required string SunHour { get; set; }

	[JsonProperty("totalSnow_cm")]
	public required string TotalSnowCm { get; set; }

	[JsonProperty("uvIndex")]
	public required string UvIndex { get; set; }
}

public partial class Astronomy
{
	[JsonProperty("moon_illumination")]
	public required string MoonIllumination { get; set; }

	[JsonProperty("moon_phase")]
	public required string MoonPhase { get; set; }

	[JsonProperty("moonrise")]
	public required string Moonrise { get; set; }

	[JsonProperty("moonset")]
	public required string Moonset { get; set; }

	[JsonProperty("sunrise")]
	public required string Sunrise { get; set; }

	[JsonProperty("sunset")]
	public required string Sunset { get; set; }
}

public partial class Hourly
{
	[JsonProperty("DewPointC")]
	public required string DewPointC { get; set; }

	[JsonProperty("DewPointF")]
	public required string DewPointF { get; set; }

	[JsonProperty("FeelsLikeC")]
	public required string FeelsLikeC { get; set; }

	[JsonProperty("FeelsLikeF")]
	public required string FeelsLikeF { get; set; }

	[JsonProperty("HeatIndexC")]
	public required string HeatIndexC { get; set; }

	[JsonProperty("HeatIndexF")]
	public required string HeatIndexF { get; set; }

	[JsonProperty("WindChillC")]
	public required string WindChillC { get; set; }

	[JsonProperty("WindChillF")]
	public required string WindChillF { get; set; }

	[JsonProperty("WindGustKmph")]
	public required string WindGustKmph { get; set; }

	[JsonProperty("WindGustMiles")]
	public required string WindGustMiles { get; set; }

	[JsonProperty("chanceoffog")]
	public required string Chanceoffog { get; set; }

	[JsonProperty("chanceoffrost")]
	public required string Chanceoffrost { get; set; }

	[JsonProperty("chanceofhightemp")]
	public required string Chanceofhightemp { get; set; }

	[JsonProperty("chanceofovercast")]
	public required string Chanceofovercast { get; set; }

	[JsonProperty("chanceofrain")]
	public required string Chanceofrain { get; set; }

	[JsonProperty("chanceofremdry")]
	public required string Chanceofremdry { get; set; }

	[JsonProperty("chanceofsnow")]
	public required string Chanceofsnow { get; set; }

	[JsonProperty("chanceofsunshine")]
	public required string Chanceofsunshine { get; set; }

	[JsonProperty("chanceofthunder")]
	public required string Chanceofthunder { get; set; }

	[JsonProperty("chanceofwindy")]
	public required string Chanceofwindy { get; set; }

	[JsonProperty("cloudcover")]
	public required string Cloudcover { get; set; }

	[JsonProperty("diffRad")]
	public required string DiffRad { get; set; }

	[JsonProperty("humidity")]
	public required string Humidity { get; set; }

	[JsonProperty("precipInches")]
	public required string PrecipInches { get; set; }

	[JsonProperty("precipMM")]
	public required string PrecipMm { get; set; }

	[JsonProperty("pressure")]
	public required string Pressure { get; set; }

	[JsonProperty("pressureInches")]
	public required string PressureInches { get; set; }

	[JsonProperty("shortRad")]
	public required string ShortRad { get; set; }

	[JsonProperty("tempC")]
	public required string TempC { get; set; }

	[JsonProperty("tempF")]
	public required string TempF { get; set; }

	[JsonProperty("time")]
	public required string Time { get; set; }

	[JsonProperty("uvIndex")]
	public required string UvIndex { get; set; }

	[JsonProperty("visibility")]
	public required string Visibility { get; set; }

	[JsonProperty("visibilityMiles")]
	public required string VisibilityMiles { get; set; }

	[JsonProperty("weatherCode")]
	public required string WeatherCode { get; set; }

	[JsonProperty("weatherDesc")]
	public required WeatherDesc[] WeatherDesc { get; set; }

	[JsonProperty("weatherIconUrl")]
	public required WeatherDesc[] WeatherIconUrl { get; set; }

	[JsonProperty("winddir16Point")]
	public required string Winddir16Point { get; set; }

	[JsonProperty("winddirDegree")]
	public required string WinddirDegree { get; set; }

	[JsonProperty("windspeedKmph")]
	public required string WindspeedKmph { get; set; }

	[JsonProperty("windspeedMiles")]
	public required string WindspeedMiles { get; set; }
}