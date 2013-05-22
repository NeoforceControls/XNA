////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Skins                                            //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: __Entry.cs                                   //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 11/09/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Copyright (c) by Tom Shane                                //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
////////////////////////////////////////////////////////////////////////////

#endregion

namespace TomShane.Neoforce.Skins
{
  
  static class Entry  
  {

    #region //// Methods ///////////

    ////////////////////////////////////////////////////////////////////////////
    static void Main(string[] args)
    {      
      #if (!XBOX && !XBOX_FAKE)
        try
        {
          System.Diagnostics.Process Proc = new System.Diagnostics.Process();
          Proc.StartInfo.FileName = "..\\..\\BuildSkins.bat";      
          Proc.Start();     
        }
        catch
        {
        }  
      #else
        Console.WriteLine("No action for Xbox platform defined.");
      #endif
       
    }
    ////////////////////////////////////////////////////////////////////////////

    #endregion    
    
  }
  
}