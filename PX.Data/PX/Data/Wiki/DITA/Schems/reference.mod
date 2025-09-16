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
<!--                   ARCHITECTURE ENTITIES                       -->
<!-- ============================================================= -->

<!-- default namespace prefix for DITAArchVersion attribute can be
     overridden through predefinition in the document type shell   -->
<!ENTITY % DITAArchNSPrefix
  "ditaarch" 
>

<!-- must be instanced on each topic type                          -->
<!ENTITY % arch-atts 
             "xmlns:%DITAArchNSPrefix; 
                        CDATA 
                                  #FIXED 'http://dita.oasis-open.org/architecture/2005/'
              %DITAArchNSPrefix;:DITAArchVersion
                        CDATA 
                                  '1.2'
  "
>


<!-- ============================================================= -->
<!--                   SPECIALIZATION OF DECLARED ELEMENTS         -->
<!-- ============================================================= -->


<!ENTITY % reference-info-types 
  "%info-types;
  " 
>


<!-- ============================================================= -->
<!--                   ELEMENT NAME ENTITIES                       -->
<!-- ============================================================= -->


<!ENTITY % reference   "reference"                                   >
<!ENTITY % refbody     "refbody"                                     >
<!ENTITY % refbodydiv  "refbodydiv"                                  >
<!ENTITY % refsyn      "refsyn"                                      >
<!ENTITY % properties  "properties"                                  >
<!ENTITY % property    "property"                                    >
<!ENTITY % proptype    "proptype"                                    >
<!ENTITY % propvalue   "propvalue"                                   >
<!ENTITY % propdesc    "propdesc"                                    >
<!ENTITY % prophead    "prophead"                                    >
<!ENTITY % proptypehd  "proptypehd"                                  >
<!ENTITY % propvaluehd "propvaluehd"                                 >
<!ENTITY % propdeschd  "propdeschd"                                  >


<!-- ============================================================= -->
<!--                    DOMAINS ATTRIBUTE OVERRIDE                 -->
<!-- ============================================================= -->


<!ENTITY included-domains 
  ""
>


<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS                       -->
<!-- ============================================================= -->


<!--                    LONG NAME: Reference                       -->
<!ENTITY % reference.content
                       "((%title;), 
                         (%titlealts;)?,
                         (%abstract; | 
                          %shortdesc;)?, 
                         (%prolog;)?, 
                         (%refbody;)?, 
                         (%related-links;)?, 
                         (%reference-info-types;)* )"
>
<!ENTITY % reference.attributes
             "id 
                        ID 
                                  #REQUIRED
              %conref-atts;
              %select-atts;
              %localization-atts;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT reference    %reference.content;>
<!ATTLIST reference
              %reference.attributes;
              %arch-atts;
              domains 
                        CDATA 
                                  "&included-domains;"
>

<!--                    LONG NAME: Reference Body                  -->
<!ENTITY % refbody.content
                       "(%data.elements.incl; | 
                         %example; | 
                         %foreign.unknown.incl; | 
                         %refbodydiv; |
                         %refsyn; | 
                         %properties; | 
                         %section; | 
                         %simpletable; | 
                         %table;)*"
>
<!ENTITY % refbody.attributes
             "%id-atts;
              %localization-atts;
              base 
                        CDATA 
                                  #IMPLIED
              %base-attribute-extensions;
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT refbody    %refbody.content;>
<!ATTLIST refbody    %refbody.attributes;>

<!--                    LONG NAME: Reference Body division         -->
<!ENTITY % refbodydiv.content
                       "(%data.elements.incl; | 
                         %example; | 
                         %foreign.unknown.incl; |
                         %refbodydiv; | 
                         %refsyn; | 
                         %properties; | 
                         %section; | 
                         %simpletable; | 
                         %table;)*"
>
<!ENTITY % refbodydiv.attributes
             "%univ-atts;
              outputclass 
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT refbodydiv    %refbodydiv.content;>
<!ATTLIST refbodydiv    %refbodydiv.attributes;>

<!--                    LONG NAME: Reference Syntax                -->
<!ENTITY % refsyn.content
                       "(%section.cnt;)*"
>
<!ENTITY % refsyn.attributes
             "spectitle 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT refsyn    %refsyn.content;>
<!ATTLIST refsyn    %refsyn.attributes;>



<!--                    LONG NAME: Properties                      -->
<!ENTITY % properties.content
                       "((%prophead;)?, 
                         (%property;)+)"
>
<!ENTITY % properties.attributes
             "relcolwidth 
                        CDATA 
                                  #IMPLIED
              keycol 
                        NMTOKEN 
                                  #IMPLIED
              refcols 
                        NMTOKENS 
                                  #IMPLIED
              spectitle 
                        CDATA 
                                  #IMPLIED
              %display-atts;
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT properties    %properties.content;>
<!ATTLIST properties    %properties.attributes;>



<!--                    LONG NAME:  Property Head                  -->
<!ENTITY % prophead.content
                       "((%proptypehd;)?, 
                         (%propvaluehd;)?, 
                         (%propdeschd;)?)"
>
<!ENTITY % prophead.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT prophead    %prophead.content;>
<!ATTLIST prophead    %prophead.attributes;>


<!--                    LONG NAME: Property Type Head              -->
<!ENTITY % proptypehd.content
                       "(%tblcell.cnt;)*"
>
<!ENTITY % proptypehd.attributes
             "specentry 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT proptypehd    %proptypehd.content;>
<!ATTLIST proptypehd    %proptypehd.attributes;>



<!--                    LONG NAME: Property Value Head             -->
<!ENTITY % propvaluehd.content
                       "(%tblcell.cnt;)*"
>
<!ENTITY % propvaluehd.attributes
             "specentry 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT propvaluehd    %propvaluehd.content;>
<!ATTLIST propvaluehd    %propvaluehd.attributes;>



<!--                    LONG NAME: Property Description Head       -->
<!ENTITY % propdeschd.content
                       "(%tblcell.cnt;)*"
>
<!ENTITY % propdeschd.attributes
             "specentry 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT propdeschd    %propdeschd.content;>
<!ATTLIST propdeschd    %propdeschd.attributes;>



<!--                    LONG NAME: Property                        -->
<!ENTITY % property.content
                       "((%proptype;)?, 
                         (%propvalue;)?, 
                         (%propdesc;)?)"
>
<!ENTITY % property.attributes
             "%univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT property    %property.content;>
<!ATTLIST property    %property.attributes;>



<!--                    LONG NAME: Property Type                   -->
<!ENTITY % proptype.content
                       "(%ph.cnt;)*"
>
<!ENTITY % proptype.attributes
             "specentry 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT proptype    %proptype.content;>
<!ATTLIST proptype    %proptype.attributes;>



<!--                    LONG NAME: Property Value                  -->
<!ENTITY % propvalue.content
                       "(%ph.cnt;)*"
>
<!ENTITY % propvalue.attributes
             "specentry 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT propvalue    %propvalue.content;>
<!ATTLIST propvalue    %propvalue.attributes;>



<!--                    LONG NAME: Property Descrption             -->
<!ENTITY % propdesc.content
                       "(%desc.cnt;)*"
>
<!ENTITY % propdesc.attributes
             "specentry 
                        CDATA 
                                  #IMPLIED
              %univ-atts; 
              outputclass 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT propdesc    %propdesc.content;>
<!ATTLIST propdesc    %propdesc.attributes;>


 

<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->


<!ATTLIST reference   %global-atts;  class  CDATA "- topic/topic       reference/reference " >
<!ATTLIST refbody     %global-atts;  class  CDATA "- topic/body        reference/refbody "   >
<!ATTLIST refbodydiv  %global-atts;  class  CDATA "- topic/bodydiv     reference/refbodydiv ">
<!ATTLIST refsyn      %global-atts;  class  CDATA "- topic/section     reference/refsyn "    >
<!ATTLIST properties  %global-atts;  class  CDATA "- topic/simpletable reference/properties ">
<!ATTLIST property    %global-atts;  class  CDATA "- topic/strow       reference/property "  >
<!ATTLIST proptype    %global-atts;  class  CDATA "- topic/stentry     reference/proptype "  >
<!ATTLIST propvalue   %global-atts;  class  CDATA "- topic/stentry     reference/propvalue " >
<!ATTLIST propdesc    %global-atts;  class  CDATA "- topic/stentry     reference/propdesc "  >

<!ATTLIST prophead    %global-atts;  class  CDATA "- topic/sthead      reference/prophead "  >
<!ATTLIST proptypehd  %global-atts;  class  CDATA "- topic/stentry     reference/proptypehd ">
<!ATTLIST propvaluehd %global-atts;  class  CDATA "- topic/stentry     reference/propvaluehd ">
<!ATTLIST propdeschd  %global-atts;  class  CDATA "- topic/stentry     reference/propdeschd ">

 
<!-- ================== End DITA Reference  =========================== -->

