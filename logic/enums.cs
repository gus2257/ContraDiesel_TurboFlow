namespace logic
{
    public enum Modulo
    {
        Configuration = 1,
        Operation = 10, // 0x0000000A
        Reports = 17, // 0x00000011
        Catalogs = 22, // 0x00000016
    }
    public enum Permiso
    {
        Trucks = 2,
        Trailers = 3,
        Mechanics = 4,
        Drivers = 5,
        WorkshopService = 6,
        General = 9,
        Manager = 13, // 0x0000000D
        Mechanic = 14, // 0x0000000E
        Inventory = 15, // 0x0000000F
        PurchaseOrders = 16, // 0x00000010
        Kardex = 18, // 0x00000012
        AvailableTractors = 19, // 0x00000013
        EFS = 20, // 0x00000014
        ServiceOrders = 21, // 0x00000015
        SecurityAccess = 23, // 0x00000017
        BrandsModels = 24, // 0x00000018
        Suppliers = 25, // 0x00000019
        WorkshopActivities = 26, // 0x0000001A
        SpareParts = 27, // 0x0000001B
        Discount = 28, // 0x0000001C
    }

    public enum SessionStateModes
    {
        SinglePage,
        AllPages,
    }
}
