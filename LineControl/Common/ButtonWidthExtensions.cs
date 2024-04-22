using System.Diagnostics.CodeAnalysis;

namespace LineControl.Common
{
  [ExcludeFromCodeCoverage]
  public static class ButtonWidthExtensions
  {
    public static string GetButtonsWidth(int noButtons)
    {
      string widthValue = null;
      switch (noButtons)
      {
        case 1:
          // calc(2 * 0.5rem (padding td) + (1 - 1) * 0.5rem (margin left button) + 1 * 2 * 0.375rem (padding button) + 1 * 2px (border button) + 1 * 1em (width icon)) + 2px (border td) + 1 * 3px (icon font-size 16 - 13)
          widthValue = "calc(1.75rem + 1em + 7px)";
          break;
        case 2:
          // calc(2 * 0.5rem (padding td) + (2 - 1) * 0.5rem (margin left button) + 2 * 2 * 0.375rem (padding button) + 2 * 2px (border button) + 2 * 1em (width icon)) + 2px (border td) + 2 * 3px (icon font-size 16 - 13)
          widthValue = "calc(3rem + 2em + 12px)";
          break;
        case 3:
          // calc(2 * 0.5rem (padding td) + (3 - 1) * 0.5rem (margin left button) + 3 * 2 * 0.375rem (padding button) + 3 * 2px (border button) + 3 * 1em (width icon)) + 2px (border td) + 3 * 3px (icon font-size 16 - 13)
          widthValue = "calc(4.25rem + 3em + 17px)";
          break;
        case 4:
          // calc(2 * 0.5rem (padding td) + (4 - 1) * 0.5rem (margin left button) + 4 * 2 * 0.375rem (padding button) + 4 * 2px (border button) + 4 * 1em (width icon)) + 2px (border td) + 4 * 3px (icon font-size 16 - 13)
          widthValue = "calc(5.5rem + 4em + 22px)";
          break;
        case 5:
          // calc(2 * 0.5rem (padding td) + (5 - 1) * 0.5rem (margin left button) + 5 * 2 * 0.375rem (padding button) + 5 * 2px (border button) + 5 * 1em (width icon)) + 2px (border td) + 5 * 3px (icon font-size 16 - 13)
          widthValue = "calc(6.75rem + 5em + 27px)";
          break;
        case 6:
          // calc(2 * 0.5rem (padding td) + (6 - 1) * 0.5rem (margin left button) + 6 * 2 * 0.375rem (padding button) + 6 * 2px (border button) + 6 * 1em (width icon)) + 2px (border td) + 6 * 3px (icon font-size 16 - 13)
          widthValue = "calc(8rem + 6em + 32px)";
          break;
        case 7:
          // calc(2 * 0.5rem (padding td) + (7 - 1) * 0.5rem (margin left button) + 7 * 2 * 0.375rem (padding button) + 7 * 2px (border button) + 7 * 1em (width icon)) + 2px (border td) + 7 * 3px (icon font-size 16 - 13)
          widthValue = "calc(9.25rem + 7em + 37px)";
          break;
      }

      return widthValue;
    }
  }
}
