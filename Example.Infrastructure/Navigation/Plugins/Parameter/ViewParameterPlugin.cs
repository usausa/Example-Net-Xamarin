namespace Example.Navigation.Plugins.Parameter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    ///
    /// </summary>
    public class ViewParameterPlugin : NavigatorPluginBase
    {
        private readonly AttributeMemberCache<ViewParameterAttribute> cache = new AttributeMemberCache<ViewParameterAttribute>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        public override void OnNavigateFrom(NavigatorPluginContext context, object view, object target)
        {
            context.Save(GetType(), GatherExportParameters(target));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="view"></param>
        /// <param name="target"></param>
        public override void OnNavigateTo(NavigatorPluginContext context, object view, object target)
        {
            var parameters = context.LoadOr(GetType(), default(Dictionary<string, object>));
            if (parameters != null)
            {
                ApplyImportParameters(target, parameters);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private Dictionary<string, object> GatherExportParameters(object target)
        {
            var parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            foreach (var member in cache.GetAttributeMembers(target.GetType()))
            {
                if ((member.Attribute.Direction & Direction.Export) != 0)
                {
                    var name = member.Attribute.Name ?? member.Name;
                    parameters.Add(name, member.GetValue(target));
                }
            }

            return parameters;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameters"></param>
        private void ApplyImportParameters(object target, IDictionary<string, object> parameters)
        {
            foreach (var member in cache.GetAttributeMembers(target.GetType()))
            {
                if ((member.Attribute.Direction & Direction.Import) != 0)
                {
                    var name = member.Attribute.Name ?? member.Name;
                    object value;
                    if (parameters.TryGetValue(name, out value))
                    {
                        member.SetValue(target, Convert.ChangeType(value, member.MemberType, CultureInfo.InvariantCulture));
                    }
                }
            }
        }
    }
}
