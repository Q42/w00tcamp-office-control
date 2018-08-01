using HueLightDJ.Effects.Base;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.Streaming.Effects;
using Q42.HueApi.Streaming.Extensions;
using Q42.HueApi.Streaming.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HueLightDJ.Effects
{
  [HueEffect(Name = "Inverse flash and fade", IsBaseEffect = false, HasColorPicker = true, DefaultColor = "#FFFFFF")]
  public class FlashFadeInverseEffect : IHueEffect
  {
    public Task Start(EntertainmentLayer layer, Func<TimeSpan> waitTime, RGBColor? color, CancellationToken cancellationToken)
    {
      //Non repeating effects should not run on baselayer
      if (layer.IsBaseLayer)
        return Task.CompletedTask;

      if (!color.HasValue)
      {
        var r = new Random();
        color = new RGBColor(r.NextDouble(), r.NextDouble(), r.NextDouble());
      }

      return layer.To2DGroup().FlashQuick(cancellationToken, color, IteratorEffectMode.All, IteratorEffectMode.All, waitTime: () => TimeSpan.FromMilliseconds(100), transitionTimeOn: () => waitTime() / 2, transitionTimeOff: () => TimeSpan.Zero, duration: waitTime() / 2);
    }
  }
}
