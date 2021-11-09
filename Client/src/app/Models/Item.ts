export class Item {
    _id: string;
    part_number: string;
    type: string;
    qty_on_hand: number;
    location: string;
    height: number;
    width: number;
    length: number;
    UOM: string;
    manufacturer: string;
    manufacturer_number: string;
    used_for: string;
    horsepower: number;
    amperage: number;
    voltage: number;
    RPM: number;
    
    constructor() {
        this._id = "";
        this.part_number = "";
        this.type = "";
        this.qty_on_hand = 0;
        this.location = "";
        this.height = 0;
        this.width = 0;
        this.length = 0;
        this.UOM = "";
        this.manufacturer = "";
        this.manufacturer_number = "";
        this.used_for = "";
        this.horsepower = 0;
        this.amperage = 0;
        this.voltage = 0;
        this.RPM = 0;
    }
}