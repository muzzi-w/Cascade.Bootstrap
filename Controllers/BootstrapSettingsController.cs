﻿using Cascade.Bootstrap.Services;
using System;
using System.Web.Mvc;

namespace Cascade.Bootstrap.Controllers
{
    public class BootstrapSettingsController : Controller
    {
        private readonly ICascadeBootstrapService _cascadeBootstrapService;

        public BootstrapSettingsController(ICascadeBootstrapService cascadeBootstrapService)
        {
            _cascadeBootstrapService = cascadeBootstrapService;
        }

        [HttpPost]
        public string DuplicateSwatch(string fromSwatch, string toSwatch)
        {
            // normalize swatch names
            fromSwatch = fromSwatch.Trim().ToLower();
            toSwatch = toSwatch.Trim().ToLower();

            // duplicate the swatch
            var bootstrapThemeFolder = Server.MapPath("/Themes/Cascade.Bootstrap");
            var message = _cascadeBootstrapService.Copy(bootstrapThemeFolder, fromSwatch, toSwatch);
            if (!String.IsNullOrEmpty(message))
                return message;

            // update the site-swatch.less file
            if (!_cascadeBootstrapService.Replace(bootstrapThemeFolder, fromSwatch, toSwatch))
                return "Unable to update swatch @import statements";

            // Success
            return null;

        }


    }
}