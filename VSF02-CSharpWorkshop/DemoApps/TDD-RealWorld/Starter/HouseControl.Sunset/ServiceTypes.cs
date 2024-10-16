namespace HouseControl.Sunset;

public record Results(string sunrise, string sunset, string solar_noon, string day_length);
public record SolarData(Results results, string status);
