﻿using DataFormatter.FormatterLogic.Model;
using Newtonsoft.Json;

namespace DataFormatter.FormatterLogic.Strategies;

public class JSONFormatterStrategy : IFormatterStrategy
{
    public string FormatText(FileData data)
    {
        return JsonConvert.SerializeObject(data);
    }
}