using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using LibraryTS;

public class posudba
{
    public static int lastAssignedId = 0;

    [JsonInclude]
    public int posudbaID { get; set; }

    [JsonInclude]
    public int bookID { get; set; }

    [JsonInclude]
    public int userID { get; set; }

    [JsonInclude]
    public DateTime datumPosudbe { get; set; }

    [JsonInclude]
    public DateTime datumVracanja { get; set; }

    public posudba()
    {
        this.posudbaID = ++lastAssignedId;
        this.bookID = bookID;
        this.userID = userID;
        this.datumPosudbe = datumPosudbe;
        this.datumVracanja = datumVracanja;
    }
}
