using System.Collections.Generic;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Models.ResultModels;

namespace Gamepad.Service.Interfaces
{
    public interface IConfigService : IBaseService<IConfigService, Config>
    {
        Config FindByKey(string key);
        string GetValue(string key);
        OperationResult Insert(string key, string value);
        OperationResult Update(string key, string value);
        OperationResult Delete(string key);
        Dictionary<string, string> GetConfigsDictionary();
    }
}