﻿using System;
using System.Threading.Tasks;

namespace Esquio.Abstractions
{
    /// <summary>
    /// Provides extensions  methods for <see cref="IFeatureService"/>.
    /// </summary>
    public static class FeatureServiceExtensions
    {
        /// <summary>
        /// Configure actions to be executed depending on the feature active state.
        /// </summary>
        /// <param name="featureService">The <see cref="IFeatureService"/>.</param>
        /// <param name="featureName">The feature name to be evaluated.</param>
        /// <param name="enabled">Action to be executed when <paramref name="featureName"/> is enabled. </param>
        /// <param name="disabled">Action to be executed when <paramref name="featureName"/> is not enabled.</param>
        /// <param name="productName">The product name.</param>
        /// <returns>A <see cref="Task"/> that complete when <paramref name="active"/> or <paramref name="disabled"/> is executed.</returns>
        public static async Task Do(this IFeatureService featureService, string featureName, Action enabled, Action disabled, string productName = null)
        {
            if (await featureService.IsEnabledAsync(featureName, productName))
            {
                enabled();
            }
            else
            {
                disabled();
            }
        }

        /// <summary>
        /// Configure actions to be executed depending on the feature active state.
        /// </summary>
        /// <typeparam name="TResult">The action return type.</typeparam>
        /// <param name="featureService">The <see cref="IFeatureService"/>.</param>
        /// <param name="featureName">The feature name to be evaluated.</param>
        /// <param name="enabled">Action to be executed when <paramref name="featureName"/> is active. </param>
        /// <param name="disabled">Action to be executed when <paramref name="featureName"/> is not active.</param>
        /// <param name="productName">The product name.</param>
        /// <returns>A <see cref="Task{TResult}"/> that complete when <paramref name="enabled"/> or <paramref name="disabled"/> is executed.</returns>
        public static async Task<TResult> Do<TResult>(this IFeatureService featureService, string featureName, Func<TResult> enabled, Func<TResult> disabled, string productName = null)
        {
            if (await featureService.IsEnabledAsync(featureName, productName))
            {
                return enabled();
            }
            else
            {
                return disabled();
            }
        }
    }
}
