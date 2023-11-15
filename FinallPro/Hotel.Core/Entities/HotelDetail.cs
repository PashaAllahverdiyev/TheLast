﻿namespace Hotel.Core.Entities;

public class HotelDetail:BaseEntity
{
    public bool CityLandScare { get; set; }
    public bool IndoorSwimmingPoor { get; set; }
    public bool FreeParking { get; set; }
    public bool FreeWiFi { get; set; }
    public bool DailyHouseKeeping { get; set; }
    public bool SafeBox { get; set; }
}
