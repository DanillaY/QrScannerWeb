using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace copyqr.Models.Entities;

public partial class User
{
    public long Id { get; set; }

    public string Login { get; set; } = null!;

    public byte[]? Hash { get;private set; }

    public virtual ICollection<QrInfo> QrInfos { get; set; } = new List<QrInfo>();

    public User() { }
    public User(byte[] hash,string login)
    {
        this.Hash = hash;
        this.Login = login;
    }
}
