////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Importers                                        //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: CursorImporter.cs                            //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 15/02/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Cursor file importer.                                     //
//                                                            //
////////////////////////////////////////////////////////////////

#region //// Using /////////////

////////////////////////////////////////////////////////////////////////////
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System;
////////////////////////////////////////////////////////////////////////////

#endregion


namespace TomShane.Neoforce.Importers
{
  

  #region //// Importer //////////
  
  public class CursorFile
  {
    public byte[] Data = null;
  }

  ////////////////////////////////////////////////////////////////////////////
  [ContentImporter(".cur", DisplayName = "Cursor - Neoforce Controls")]
  class CursorImporter: ContentImporter<CursorFile>
  {
		#region //// Methods ///////////
			
		////////////////////////////////////////////////////////////////////////////
		public override CursorFile Import(string filename, ContentImporterContext context)
    {             
      CursorFile cur = new CursorFile();
      cur.Data = File.ReadAllBytes(filename);     
      return cur;
    }
		////////////////////////////////////////////////////////////////////////////
			
  	#endregion  
  }  	
  ////////////////////////////////////////////////////////////////////////////

  #endregion

  #region //// Writer ////////////

  ////////////////////////////////////////////////////////////////////////////
  [ContentTypeWriter]
  class CursorWriter: ContentTypeWriter<CursorFile>
  {

    #region //// Methods ///////////
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Write(ContentWriter output, CursorFile value)
    {      
      output.Write(value.Data.Length);
      output.Write(value.Data);
    }
    ///////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
      return typeof(CursorFile).AssemblyQualifiedName;
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////    
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
      return "TomShane.Neoforce.Controls.CursorReader, TomShane.Neoforce.Controls";
    }      
    ////////////////////////////////////////////////////////////////////////////

    #endregion
  } 
  ////////////////////////////////////////////////////////////////////////////

  #endregion
  
}