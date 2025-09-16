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

<!ENTITY % b           "b"                                           >
<!ENTITY % i           "i"                                           >
<!ENTITY % u           "u"                                           > 
<!ENTITY % tt          "tt"                                          >
<!ENTITY % sup         "sup"                                         >
<!ENTITY % sub         "sub"                                         >


<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS                       -->
<!-- ============================================================= -->


<!--                    LONG NAME: Bold                            -->
<!ENTITY % b.content
                       "(#PCDATA | 
                         %basic.ph; | 
                         %data.elements.incl; |
                         %foreign.unknown.incl;)*"
>
<!ENTITY % b.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT b    %b.content;>
<!ATTLIST b    %b.attributes;>



<!--                    LONG NAME: Underlined                      -->
<!ENTITY % u.content
                       "(#PCDATA | 
                         %basic.ph; | 
                         %data.elements.incl; |
                         %foreign.unknown.incl;)*"
>
<!ENTITY % u.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT u    %u.content;>
<!ATTLIST u    %u.attributes;>



<!--                    LONG NAME: Italic                          -->
<!ENTITY % i.content
                       "(#PCDATA | 
                         %basic.ph; | 
                         %data.elements.incl; |
                         %foreign.unknown.incl;)*"
>
<!ENTITY % i.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT i    %i.content;>
<!ATTLIST i    %i.attributes;>



<!--                    LONG NAME: Teletype (monospaced)           -->
<!ENTITY % tt.content
                       "(#PCDATA | 
                         %basic.ph; | 
                         %data.elements.incl; |
                         %foreign.unknown.incl;)*"
>
<!ENTITY % tt.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT tt    %tt.content;>
<!ATTLIST tt    %tt.attributes;>



<!--                    LONG NAME: Superscript                     -->
<!ENTITY % sup.content
                       "(#PCDATA | 
                         %basic.ph; | 
                         %data.elements.incl; |
                         %foreign.unknown.incl;)*"
>
<!ENTITY % sup.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT sup    %sup.content;>
<!ATTLIST sup    %sup.attributes;>



<!--                    LONG NAME: Subscript                       -->
<!ENTITY % sub.content
                       "(#PCDATA | 
                         %basic.ph; | 
                         %data.elements.incl; |
                         %foreign.unknown.incl;)*"
>
<!ENTITY % sub.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT sub    %sub.content;>
<!ATTLIST sub    %sub.attributes;>

 

<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->


<!ATTLIST b           %global-atts;  class CDATA "+ topic/ph hi-d/b "  >
<!ATTLIST i           %global-atts;  class CDATA "+ topic/ph hi-d/i "  >
<!ATTLIST sub         %global-atts;  class CDATA "+ topic/ph hi-d/sub ">
<!ATTLIST sup         %global-atts;  class CDATA "+ topic/ph hi-d/sup ">
<!ATTLIST tt          %global-atts;  class CDATA "+ topic/ph hi-d/tt " >
<!ATTLIST u           %global-atts;  class CDATA "+ topic/ph hi-d/u "  >


<!-- ================== DITA Highlight Domain ==================== -->