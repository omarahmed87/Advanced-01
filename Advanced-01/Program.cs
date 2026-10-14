using System;

#region DeliveryAddress Class
public class DeliveryAddress : ICloneable
{
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int BuildingNumber { get; set; }

    public DeliveryAddress(string city, string street, int buildingNumber)
    {
        City = city;
        Street = street;
        BuildingNumber = buildingNumber;
    }

    public DeliveryAddress(DeliveryAddress other)
    {
        if (other != null)
        {
            City = other.City;
            Street = other.Street;
            BuildingNumber = other.BuildingNumber;
        }
    }

    public object Clone()
    {
        return new DeliveryAddress(this.City, this.Street, this.BuildingNumber);
    }

    public string GetFullAddress()
    {
        return $"{BuildingNumber} {Street}, {City}";
    }
}
#endregion

#region Driver Class
public class Driver
{
    public string Name { get; set; } = string.Empty;

    public Driver(string name)
    {
        Name = name;
    }
}
#endregion

#region Shipment Base Class (Partial Class - Q9)
public abstract partial class Shipment : ICloneable
{
    // Q4: Static field to keep track of total shipments created
    private static int totalShipmentsCreated;

    public static int TotalShipmentsCreated => totalShipmentsCreated;

    // Q5: Static Constructor
    static Shipment()
    {
        totalShipmentsCreated = 0;
        Console.WriteLine("Shipment System Initialized");
    }

    // Q6: Static Method to return total shipments created
    public static int GetTotalShipmentsCreated()
    {
        return totalShipmentsCreated;
    }

    private string trackingCode;
    private string description;
    private decimal weight;
    private decimal deliveryFee;

    public string TrackingCode => trackingCode;

    public string Description
    {
        get => description;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                description = value;
        }
    }

    public decimal Weight
    {
        get => weight;
        set
        {
            if (value > 0)
                weight = value;
        }
    }

    public decimal DeliveryFee
    {
        get => deliveryFee;
        private set
        {
            if (value > 0)
                deliveryFee = value;
        }
    }

    public DeliveryAddress? Destination { get; set; }

    public abstract decimal EstimatedCost { get; }

    // Constructor increments static counter on every creation (Q4)
    public Shipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress? destination)
    {
        this.trackingCode = string.IsNullOrWhiteSpace(trackingCode) ? "UNKNOWN" : trackingCode;
        this.description = !string.IsNullOrWhiteSpace(description) ? description : "Unknown";
        this.weight = weight > 0 ? weight : 1;
        this.deliveryFee = deliveryFee > 0 ? deliveryFee : 50;
        this.Destination = destination;

        totalShipmentsCreated++;
    }

    // Q1: Object Copying method
    public virtual Shipment CopyShipment()
    {
        return (Shipment)this.Clone();
    }

    // Q2: Shallow Copy using MemberwiseClone()
    public Shipment ShallowCopy()
    {
        return (Shipment)this.MemberwiseClone();
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    // Q3: Deep Copy abstract method
    public abstract Shipment DeepCopy();

    public void UpdateWeight(decimal newWeight)
    {
        if (newWeight > 0)
            Weight = newWeight;
    }

    public virtual void PrintShipment()
    {
        Console.WriteLine($"Tracking Code : {TrackingCode}");
        Console.WriteLine($"Description   : {Description}");
        Console.WriteLine($"Destination   : {Destination?.GetFullAddress()}");
        Console.WriteLine($"Weight        : {Weight} KG");
        Console.WriteLine($"Delivery Fee  : {DeliveryFee} EGP");
        Console.WriteLine($"Estimated Cost: {EstimatedCost} EGP");
        Console.WriteLine($"Status        : {GetTrackingStatus()}");
    }
}
#endregion

#region Shipment Tracking & Partial Method (Partial Class - Q9 & Q10)
public abstract partial class Shipment
{
    private string trackingStatus = "In Transit";

    public string TrackingStatus
    {
        get => trackingStatus;
        set => trackingStatus = value;
    }

    public string GetTrackingStatus()
    {
        return TrackingStatus;
    }

    public void UpdateTrackingStatus(string newStatus)
    {
        if (!string.IsNullOrWhiteSpace(newStatus))
        {
            TrackingStatus = newStatus;
            // Q10: Calling partial method
            OnTrackingStatusChanged(newStatus);
        }
    }

    // Q10: Declaration of partial method
    partial void OnTrackingStatusChanged(string newStatus);
}

public abstract partial class Shipment
{
    // Q10: Implementation of partial method
    partial void OnTrackingStatusChanged(string newStatus)
    {
        Console.WriteLine($"Tracking status changed to: {newStatus}");
    }
}
#endregion

#region Derived Shipment Classes (Standard, Express, International)
public class StandardShipment : Shipment
{
    public override decimal EstimatedCost => DeliveryFee + (Weight * 5);

    public StandardShipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress? destination)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
    }

    // Q3: Deep Copy Implementation
    public override Shipment DeepCopy()
    {
        DeliveryAddress? newAddress = (DeliveryAddress?)this.Destination?.Clone();
        StandardShipment copy = new StandardShipment(this.TrackingCode, this.Description, this.Weight, this.DeliveryFee, newAddress);
        copy.TrackingStatus = this.TrackingStatus;
        return copy;
    }

    public override void PrintShipment()
    {
        Console.WriteLine("Standard Shipment:");
        base.PrintShipment();
    }
}

public class ExpressShipment : Shipment
{
    private decimal extraFee;

    public decimal ExtraFee
    {
        get => extraFee;
        set { if (value >= 0) extraFee = value; }
    }

    public override decimal EstimatedCost => DeliveryFee + (Weight * 5) + ExtraFee;

    public ExpressShipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress? destination, decimal extraFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        ExtraFee = extraFee;
    }

    // Q3: Deep Copy Implementation
    public override Shipment DeepCopy()
    {
        DeliveryAddress? newAddress = (DeliveryAddress?)this.Destination?.Clone();
        ExpressShipment copy = new ExpressShipment(this.TrackingCode, this.Description, this.Weight, this.DeliveryFee, newAddress, this.ExtraFee);
        copy.TrackingStatus = this.TrackingStatus;
        return copy;
    }

    public override void PrintShipment()
    {
        Console.WriteLine("Express Shipment:");
        base.PrintShipment();
        Console.WriteLine($"Extra Fee     : {ExtraFee} EGP");
    }
}

public class InternationalShipment : Shipment
{
    private decimal customsFee;
    public string DestinationCountry { get; set; } = string.Empty;

    public decimal CustomsFee
    {
        get => customsFee;
        set { if (value >= 0) customsFee = value; }
    }

    public override decimal EstimatedCost => DeliveryFee + (Weight * 5) + CustomsFee;

    public InternationalShipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress? destination, string destinationCountry, decimal customsFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        DestinationCountry = destinationCountry;
        CustomsFee = customsFee;
    }

    // Q3: Deep Copy Implementation
    public override Shipment DeepCopy()
    {
        DeliveryAddress? newAddress = (DeliveryAddress?)this.Destination?.Clone();
        InternationalShipment copy = new InternationalShipment(this.TrackingCode, this.Description, this.Weight, this.DeliveryFee, newAddress, this.DestinationCountry, this.CustomsFee);
        copy.TrackingStatus = this.TrackingStatus;
        return copy;
    }

    public override void PrintShipment()
    {
        Console.WriteLine("International Shipment:");
        base.PrintShipment();
        Console.WriteLine($"Country       : {DestinationCountry}");
        Console.WriteLine($"Customs Fee   : {CustomsFee} EGP");
    }
}
#endregion

#region DeliveryCenter Class
public class DeliveryCenter
{
    public string CenterName { get; set; } = "Main Center";
    public Driver? CenterDriver { get; set; }
    private Shipment[] shipments = new Shipment[20];
    private int count = 0;

    public DeliveryCenter(string centerName = "Main Center")
    {
        CenterName = centerName;
    }

    public bool AddShipment(Shipment? shipment)
    {
        if (shipment == null) return false;
        if (count < shipments.Length)
        {
            shipments[count++] = shipment;
            return true;
        }
        return false;
    }

    public void PrintAllShipments()
    {
        Console.WriteLine($"--- {CenterName} ---");
        for (int i = 0; i < count; i++)
        {
            shipments[i]?.PrintShipment();
            Console.WriteLine();
        }
    }
}
#endregion

#region Q7: Static Class - DeliveryUtilities
public static class DeliveryUtilities
{
    public static void PrintSeparator()
    {
        Console.WriteLine("==========================================");
    }

    public static void PrintSystemTitle(string title)
    {
        Console.WriteLine("------------------------------------------");
        Console.WriteLine(title);
        Console.WriteLine("------------------------------------------");
    }
}
#endregion

#region Q8: Extension Methods - ShipmentExtensions
public static class ShipmentExtensions
{
    // Q8: GetSummary Extension Method
    public static string GetSummary(this Shipment shipment)
    {
        if (shipment == null) return string.Empty;

        string shipmentType = "Shipment";
        if (shipment is StandardShipment) shipmentType = "Standard";
        else if (shipment is ExpressShipment) shipmentType = "Express";
        else if (shipment is InternationalShipment) shipmentType = "International";

        return $"{shipment.TrackingCode} | {shipmentType} | {shipment.Weight} KG | {shipment.GetTrackingStatus()}";
    }

    // Q8: IsDelivered Extension Method
    public static bool IsDelivered(this Shipment shipment)
    {
        return shipment != null && shipment.GetTrackingStatus().Equals("Delivered", StringComparison.OrdinalIgnoreCase);
    }
}
#endregion

public class Program
{
    public static void Main()
    {
        // Q7: Using Static Class DeliveryUtilities
        DeliveryUtilities.PrintSystemTitle("Smart Delivery Management System");

        DeliveryUtilities.PrintSeparator();
        Console.WriteLine("Creating Shipments...");
        DeliveryUtilities.PrintSeparator();

        DeliveryAddress addr1 = new DeliveryAddress("Cairo", "Tahrir St", 10);
        DeliveryAddress addr2 = new DeliveryAddress("Giza", "Haram St", 25);
        DeliveryAddress addr3 = new DeliveryAddress("Alexandria", "Corniche St", 5);

        StandardShipment standard = new StandardShipment("SH001", "Laptop", 3m, 80m, addr1);
        Console.WriteLine("Standard Shipment Created");

        ExpressShipment express = new ExpressShipment("SH002", "Mobile Phone", 2m, 60m, addr2, 30m);
        Console.WriteLine("Express Shipment Created");

        InternationalShipment international = new InternationalShipment("SH003", "Television", 8m, 120m, addr3, "Germany", 100m);
        Console.WriteLine("International Shipment Created");

        // Q6: Calling Static Method GetTotalShipmentsCreated()
        Console.WriteLine($"Total Shipments Created : {Shipment.GetTotalShipmentsCreated()}");

        // Q1: Object Copying Demonstration
        DeliveryUtilities.PrintSeparator();
        Console.WriteLine("Object Copying (Q1)");
        DeliveryUtilities.PrintSeparator();
        Shipment shipment1 = standard;
        Shipment shipment2 = shipment1;
        Console.WriteLine($"Original Shipment  : {shipment1.TrackingCode}");
        Console.WriteLine($"Assigned Shipment  : {shipment2.TrackingCode}");
        Console.WriteLine($"Same Object : {ReferenceEquals(shipment1, shipment2)}");

        // Q2: Shallow Copy Demonstration
        DeliveryUtilities.PrintSeparator();
        Console.WriteLine("Shallow Copy (Q2)");
        DeliveryUtilities.PrintSeparator();
        StandardShipment shallowOriginal = new StandardShipment("SH004", "Keyboard", 1m, 40m, new DeliveryAddress("Cairo", "Tahrir St", 10));
        StandardShipment shallowCopied = (StandardShipment)shallowOriginal.ShallowCopy();
        Console.WriteLine($"Original City : {shallowOriginal.Destination?.City}");
        shallowCopied.Destination!.City = "Giza";
        Console.WriteLine($"Original City After Shallow Copy Modification : {shallowOriginal.Destination?.City} (Affected!)");
        Console.WriteLine($"Same Address Object : {ReferenceEquals(shallowOriginal.Destination, shallowCopied.Destination)}");

        // Q3: Deep Copy Demonstration
        DeliveryUtilities.PrintSeparator();
        Console.WriteLine("Deep Copy (Q3)");
        DeliveryUtilities.PrintSeparator();
        StandardShipment deepOriginal = new StandardShipment("SH005", "Mouse", 1m, 30m, new DeliveryAddress("Cairo", "Tahrir St", 10));
        StandardShipment deepCopied = (StandardShipment)deepOriginal.DeepCopy();
        Console.WriteLine($"Original City : {deepOriginal.Destination?.City}");
        deepCopied.Destination!.City = "Giza";
        Console.WriteLine($"Original City After Deep Copy Modification : {deepOriginal.Destination?.City} (Unaffected!)");
        Console.WriteLine($"Same Address Object : {ReferenceEquals(deepOriginal.Destination, deepCopied.Destination)}");

        // Q8: Extension Methods Demonstration
        DeliveryUtilities.PrintSeparator();
        Console.WriteLine("Extension Methods (Q8)");
        DeliveryUtilities.PrintSeparator();
        standard.UpdateTrackingStatus("In Transit");
        express.UpdateTrackingStatus("Out For Delivery");
        international.UpdateTrackingStatus("Delivered");

        Console.WriteLine(standard.GetSummary());
        Console.WriteLine(express.GetSummary());
        Console.WriteLine(international.GetSummary());

        Console.WriteLine($"SH001 Is Delivered : {standard.IsDelivered()}");
        Console.WriteLine($"SH003 Is Delivered : {international.IsDelivered()}");

        // Q10: Partial Method Demonstration
        DeliveryUtilities.PrintSeparator();
        Console.WriteLine("Partial Method Demonstration (Q10)");
        DeliveryUtilities.PrintSeparator();
        standard.UpdateTrackingStatus("Delivered");

        DeliveryUtilities.PrintSeparator();
        Console.WriteLine("Assignment Completed Successfully!");
        DeliveryUtilities.PrintSeparator();
    }
}