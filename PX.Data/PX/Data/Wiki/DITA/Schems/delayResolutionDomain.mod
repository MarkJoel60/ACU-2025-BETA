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

<!ENTITY % exportanchors "exportanchors"                             >
<!ENTITY % anchorid      "anchorid"                                  >
<!ENTITY % anchorkey     "anchorkey"                                 >


<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS                       -->
<!-- ============================================================= -->


<!--                    LONG NAME: Export Anchor List              -->
<!ENTITY % exportanchors.content
                       "(%anchorid; | 
                         %anchorkey;)*"
>
<!ENTITY % exportanchors.attributes
             "%univ-atts;"
>
<!ELEMENT exportanchors    %exportanchors.content;>
<!ATTLIST exportanchors    %exportanchors.attributes;>


<!--                    LONG NAME: Anchor ID                       -->
<!ENTITY % anchorid.content
                       "EMPTY"
>
<!ENTITY % anchorid.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              id 
                        NMTOKEN 
                                  #REQUIRED
              %conref-atts;
              %select-atts;
              %localization-atts;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT anchorid    %anchorid.content;>
<!ATTLIST anchorid    %anchorid.attributes;>


<!--                    LONG NAME: Anchor Key                       -->
<!ENTITY % anchorkey.content
                       "EMPTY"
>
<!ENTITY % anchorkey.attributes
             "keyref 
                        CDATA 
                                  #REQUIRED
              %univ-atts;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT anchorkey    %anchorkey.content;>
<!ATTLIST anchorkey    %anchorkey.attributes;>



<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->

<!ATTLIST exportanchors %global-atts;  class CDATA "+ topic/keywords delay-d/exportanchors "  >
<!ATTLIST anchorid      %global-atts;  class CDATA "+ topic/keyword delay-d/anchorid "  >
<!ATTLIST anchorkey     %global-atts;  class CDATA "+ topic/keyword delay-d/anchorkey "  >

<!-- ================== End Delayed Resolution Domain  =========== -->
