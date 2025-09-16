<?xml version="1.0" encoding="UTF-8"?>
<!-- ============================================================= -->
<!--                        Acumatica Inc.                         -->
<!--          Copyright (c) 1994-2011 All rights reserved.         -->
<!--                                                               -->
<!--                                                               -->
<!-- This file and its contents are protected by United States     -->
<!-- and International copyright laws.  Unauthorized reproduction  -->
<!-- and/or distribution of all or any portion of the code         -->
<!-- contained here in is strictly prohibited and will result in   -->
<!-- severe civil and criminal penalties.                          -->
<!-- Any violations of this copyright will be prosecuted       	   -->
<!-- to the fullest extent possible under law.                     -->
<!--                                                               -->
<!-- UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE   -->
<!-- OR IN PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES -->
<!-- THE SAME, OR SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY     -->
<!-- ProjectX PRODUCT.                                             -->
<!-- THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.      -->
<!-- ============================================================= -->


<!-- ============================================================= -->
<!--                   ELEMENT NAME ENTITIES                       -->
<!-- ============================================================= -->

<!ENTITY % imagemap    "imagemap"                                    >
<!ENTITY % area        "area"                                        >
<!ENTITY % shape       "shape"                                       >
<!ENTITY % coords      "coords"                                      >


<!-- ============================================================= -->
<!--                    COMMON ATTLIST SETS                        -->
<!-- ============================================================= -->


<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS for IMAGEMAP          -->
<!-- ============================================================= -->


<!--                    LONG NAME: Imagemap                        -->
<!ENTITY % imagemap.content
                       "((%image;), 
                         (%area;)+)"
>
<!ENTITY % imagemap.attributes
             "%display-atts;
              spectitle 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT imagemap    %imagemap.content;>
<!ATTLIST imagemap    %imagemap.attributes;>



<!--                    LONG NAME: Hotspot Area Description        -->
<!ENTITY % area.content
                       "((%shape;), 
                         (%coords;), 
                         (%xref;))"
>
<!ENTITY % area.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT area    %area.content;>
<!ATTLIST area    %area.attributes;>



<!--                    LONG NAME: Shape of the Hotspot            -->
<!ENTITY % shape.content
                       "(#PCDATA |
                         %text;)*
">
<!ENTITY % shape.attributes
             "keyref 
                        CDATA
                                  #IMPLIED
              %univ-atts-translate-no; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT shape    %shape.content;>
<!ATTLIST shape    %shape.attributes;>



<!--                    LONG NAME: Coordinates of the Hotspot      -->
<!ENTITY % coords.content
                       "(%words.cnt;)*"
>
<!ENTITY % coords.attributes
             "keyref
                        CDATA
                                  #IMPLIED
              %univ-atts-translate-no;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT coords    %coords.content;>
<!ATTLIST coords    %coords.attributes;>

 

<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->


<!ATTLIST imagemap %global-atts;  class CDATA "+ topic/fig ut-d/imagemap " >
<!ATTLIST area     %global-atts;  class CDATA "+ topic/figgroup ut-d/area ">
<!ATTLIST shape    %global-atts;  class CDATA "+ topic/keyword ut-d/shape ">
<!ATTLIST coords   %global-atts;  class CDATA "+ topic/ph ut-d/coords "    >

 
<!-- ================== End Utilities Domain ====================== -->