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
<!--                    COMMON ATTLIST SETS                        -->
<!-- ============================================================= -->

<!-- ============================================================= -->
<!--                    Hazard Statement Entities                  -->
<!-- ============================================================= -->

<!ENTITY % hazard.cnt 
  "#PCDATA | 
   %basic.ph; | 
   %sl; | 
   %simpletable;"
>

<!-- ============================================================= -->
<!--                   ELEMENT NAME ENTITIES                       -->
<!-- ============================================================= -->

 
<!ENTITY % hazardstatement "hazardstatement"                         >
<!ENTITY % messagepanel    "messagepanel"                            >
<!ENTITY % hazardsymbol    "hazardsymbol"                            >
<!ENTITY % typeofhazard    "typeofhazard"                            >
<!ENTITY % consequence     "consequence"                             >
<!ENTITY % howtoavoid      "howtoavoid"                              >

<!-- ============================================================= -->
<!--            HAZARDSTATEMENT KEYWORD TYPES ELEMENT DECLARATIONS -->
<!-- ============================================================= -->


<!--                    LONG NAME: Hazard Statement                -->
<!ENTITY % hazardstatement.content
                       "((%messagepanel;)+,
                         (%hazardsymbol;)*)"
>
<!ENTITY % hazardstatement.attributes
             "type 
                        (attention|
                         caution | 
                         danger | 
                         fastpath | 
                         important | 
                         note |
                         notice |
                         other | 
                         remember | 
                         restriction |
                         tip |
                         warning |
                         -dita-use-conref-target) 
                                  #IMPLIED 
              spectitle 
                        CDATA 
                                  #IMPLIED
              othertype 
                        CDATA 
                                  #IMPLIED
              %univ-atts;
              outputclass
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT hazardstatement    %hazardstatement.content;>
<!ATTLIST hazardstatement    %hazardstatement.attributes;>


<!--                    LONG NAME: Hazard Symbol                   -->
<!ENTITY % hazardsymbol.content
                       "((%alt;)?,
                         (%longdescref;)?)"
>
<!ENTITY % hazardsymbol.attributes
             "href 
                        CDATA 
                                  #REQUIRED

              scope 
                        (external | 
                         local | 
                         peer | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED
              longdescref 
                        CDATA 
                                  #IMPLIED
              height 
                        NMTOKEN 
                                  #IMPLIED
              width 
                        NMTOKEN 
                                  #IMPLIED
              align 
                        CDATA 
                                  #IMPLIED
              scale 
                        NMTOKEN 
                                  #IMPLIED
              scalefit
                        (yes |
                         no |
                         -dita-use-conref-target)
                                  #IMPLIED
              placement 
                        (break | 
                         inline | 
                         -dita-use-conref-target) 
                                  'inline'
              %univ-atts;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT hazardsymbol    %hazardsymbol.content;>
<!ATTLIST hazardsymbol    %hazardsymbol.attributes;>


<!--                    LONG NAME: Hazard Message panel            -->
<!ENTITY % messagepanel.content
                       "((%typeofhazard;),
                         (%consequence;)*,
                         (%howtoavoid;)+)"
>
<!ENTITY % messagepanel.attributes
             "spectitle 
                        CDATA 
                                  #IMPLIED
              %univ-atts;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT messagepanel    %messagepanel.content;>
<!ATTLIST messagepanel    %messagepanel.attributes;>



<!--                    LONG NAME: The Type of Hazard              -->
<!ENTITY % typeofhazard.content
                   "(%words.cnt; |
                     %ph; |
                     %tm;)*"
>
<!ENTITY % typeofhazard.attributes
             "%univ-atts;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT typeofhazard    %typeofhazard.content;>
<!ATTLIST typeofhazard    %typeofhazard.attributes;>



<!--            LONG NAME: Consequences of not Avoiding the Hazard -->
<!ENTITY % consequence.content
                       "(%words.cnt; |
                         %ph; | 
                         %tm;)*
">
<!ENTITY % consequence.attributes
             "%univ-atts;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT consequence    %consequence.content;>
<!ATTLIST consequence    %consequence.attributes;>


<!--                    LONG NAME: How to Avoid the Hazard         -->
<!ENTITY % howtoavoid.content
                       "(%hazard.cnt;)*
">
<!ENTITY % howtoavoid.attributes
             "%univ-atts;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT howtoavoid    %howtoavoid.content;>
<!ATTLIST howtoavoid    %howtoavoid.attributes;>


<!-- ============================================================= -->
<!--               SPECIALIZATION ATTRIBUTE DECLARATIONS           -->
<!-- ============================================================= -->

<!ATTLIST hazardstatement %global-atts;  class CDATA "+ topic/note hazard-d/hazardstatement "> 
<!ATTLIST messagepanel    %global-atts;  class CDATA "+ topic/ul hazard-d/messagepanel "     > 
<!ATTLIST hazardsymbol    %global-atts;  class CDATA "+ topic/image hazard-d/hazardsymbol "  > 
<!ATTLIST typeofhazard    %global-atts;  class CDATA "+ topic/li hazard-d/typeofhazard "     >
<!ATTLIST consequence     %global-atts;  class CDATA "+ topic/li hazard-d/consequence "      >
<!ATTLIST howtoavoid      %global-atts;  class CDATA "+ topic/li hazard-d/howtoavoid "       >
 
<!-- ================== End DITA Hazard Statement Domain ========= -->
