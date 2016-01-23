using System;
using System.Collections.Generic;
using System.Linq;
using Gamepad.Service.Data;
using Gamepad.Service.Data.Entities;
using Gamepad.Service.Interfaces;
using Gamepad.Service.Models.ResultModels;
using Gamepad.Service.Resources;

namespace Gamepad.Service.Services
{
    internal class ConfigService : BaseService<Config>, IConfigService
    {
        public ConfigService(GamepadContext context) : base(context)
        {
        }

        public IConfigService Clone()
        {
            return new ConfigService(new GamepadContext());
        }


        public Config FindByKey(string key)
        {
            return Get(x => x.Key == key);
        }

        public string GetValue(string key)
        {
            return FindByKey(key)?.Value;
        }

        public OperationResult Insert(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key) || key.Length < 2 || key.Length > 100)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_InputData);
            }
            var config = FindByKey(key);
            if (config != null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }
            var item = new Config
            {
                Key = key,
                Value = value
            };
            return base.Insert(item);
        }

        public override OperationResult Insert(Config item)
        {
            return Insert(item.Key, item.Value);
        }

        public OperationResult Update(string key, string value)
        {
            var config = FindByKey(key);
            if (config == null)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound);
            }
            config.Value = value;
            config.EditDate = DateTime.Now;
            return base.Update(config);
        }

        public override OperationResult Update(Config item)
        {
            var config = FindByKey(item.Key);
            if (config != null && config.Id != item.Id)
            {
                return OperationResult.Failed(ErrorMessages.Services_General_Duplicate);
            }
            item.EditDate = DateTime.Now;
            return base.Update(item);
        }

        public OperationResult Delete(string key)
        {
            var config = FindByKey(key);
            return config == null
                ? OperationResult.Failed(ErrorMessages.Services_General_ItemNotFound)
                : base.Delete(config);
        }

        public Dictionary<string, string> GetConfigsDictionary()
        {
            return Search(x => true).OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}