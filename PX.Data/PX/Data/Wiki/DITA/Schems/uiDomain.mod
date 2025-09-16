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

 
<!ENTITY % uicontrol   "uicontrol"                                   >
<!ENTITY % wintitle    "wintitle"                                    >
<!ENTITY % menucascade "menucascade"                                 >
<!ENTITY % shortcut    "shortcut"                                    >
<!ENTITY % screen      "screen"                                      >


<!-- ============================================================= -->
<!--                    UI KEYWORD TYPES ELEMENT DECLARATIONS      -->
<!-- ============================================================= -->


<!--                    LONG NAME: User Interface Control          -->
<!ENTITY % uicontrol.content
                       "(%words.cnt; | 
                         %image; | 
                         %shortcut;)*"
>
<!ENTITY % uicontrol.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT uicontrol    %uicontrol.content;>
<!ATTLIST uicontrol    %uicontrol.attributes;>



<!--                    LONG NAME: Window Title                    -->
<!ENTITY % wintitle.content
                       "(#PCDATA |
                         %text;)*
">
<!ENTITY % wintitle.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT wintitle    %wintitle.content;>
<!ATTLIST wintitle    %wintitle.attributes;>




<!--                    LONG NAME: Menu Cascade                    -->
<!ENTITY % menucascade.content
                       "(%uicontrol;)+"
>
<!ENTITY % menucascade.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT menucascade    %menucascade.content;>
<!ATTLIST menucascade    %menucascade.attributes;>



<!--                    LONG NAME: Short Cut                       -->
<!ENTITY % shortcut.content
                       "(#PCDATA |
                        %text;)*
">
<!ENTITY % shortcut.attributes
             "keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT shortcut    %shortcut.content;>
<!ATTLIST shortcut    %shortcut.attributes;>



<!--                    LONG NAME: Text Screen Capture             -->
<!ENTITY % screen.content
                       "(#PCDATA | 
                         %basic.ph.notm; |
                         %data.elements.incl; | 
                         %foreign.unknown.incl; | 
                         %txt.incl;)*"
>
<!ENTITY % screen.attributes
             "%display-atts;
              spectitle 
                        CDATA 
                                  #IMPLIED
              xml:space 
                        (preserve) 
                                  #FIXED 'preserve'
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT screen    %screen.content;>
<!ATTLIST screen    %screen.attributes;>

 

<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->
 

<!ATTLIST menucascade %global-atts;  class CDATA "+ topic/ph ui-d/menucascade "  >
<!ATTLIST screen      %global-atts;  class CDATA "+ topic/pre ui-d/screen "      >
<!ATTLIST shortcut    %global-atts;  class CDATA "+ topic/keyword ui-d/shortcut ">
<!ATTLIST uicontrol   %global-atts;  class CDATA "+ topic/ph ui-d/uicontrol "    >
<!ATTLIST wintitle    %global-atts;  class CDATA "+ topic/keyword ui-d/wintitle ">

 
<!-- ================== End DITA User Interface Domain =========== -->
