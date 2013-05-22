////////////////////////////////////////////////////////////////
//                                                            //
//  Neoforce Importers                                        //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//         File: LayoutImporter.cs                            //
//                                                            //
//      Version: 0.7                                          //
//                                                            //
//         Date: 15/02/2010                                   //
//                                                            //
//       Author: Tom Shane                                    //
//                                                            //
////////////////////////////////////////////////////////////////
//                                                            //
//  Layout file importer.                                     //
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
////////////////////////////////////////////////////////////////////////////

#endregion


namespace TomShane.Neoforce.Importers
{

  #region //// Importer //////////

  ////////////////////////////////////////////////////////////////////////////
  public class LayoutXmlDocument : XmlDocument { }
  ////////////////////////////////////////////////////////////////////////////
  
  ////////////////////////////////////////////////////////////////////////////
  [ContentImporter(".xml", DisplayName = "Layout - Neoforce Controls")]
  class LayoutImporter: ContentImporter<LayoutXmlDocument>
  {
		#region //// Methods ///////////
			
		////////////////////////////////////////////////////////////////////////////
		public override LayoutXmlDocument Import(string filename, ContentImporterContext context)
    {      
      LayoutXmlDocument doc = new LayoutXmlDocument();            
      doc.Load(filename);

      return doc;
    }
		////////////////////////////////////////////////////////////////////////////
			
  	#endregion  
  }  	
  ////////////////////////////////////////////////////////////////////////////

  #endregion

  #region //// Writer ////////////

  ////////////////////////////////////////////////////////////////////////////
  [ContentTypeWriter]
  class LayoutWriter:ContentTypeWriter<LayoutXmlDocument>
  {

    #region //// Methods ///////////
    
    ////////////////////////////////////////////////////////////////////////////
    protected override void Write(ContentWriter output, LayoutXmlDocument value)
    {      
      output.Write(value.InnerXml);              
    }
    ///////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////
    public override string GetRuntimeType(TargetPlatform targetPlatform)
    {
      return typeof(LayoutXmlDocument).AssemblyQualifiedName;
    }
    ////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////    
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {      
      if (targetPlatform == TargetPlatform.Xbox360)
      {
        return "TomShane.Neoforce.Controls.LayoutReader, TomShane.Neoforce.Controls.360";
      } 
      else
      {
        return "TomShane.Neoforce.Controls.LayoutReader, TomShane.Neoforce.Controls";
      } 
    }      
    ////////////////////////////////////////////////////////////////////////////

    #endregion
  } 
  ////////////////////////////////////////////////////////////////////////////

  #endregion
  
}