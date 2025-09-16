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
<!--                   ELEMENT NAME ENTITIES                       -->
<!-- ============================================================= -->


<!ENTITY % map         "map"                                         >
<!ENTITY % anchor      "anchor"                                      >
<!ENTITY % linktext    "linktext"                                    >
<!ENTITY % navref      "navref"                                      >
<!ENTITY % relcell     "relcell"                                     >
<!ENTITY % relcolspec  "relcolspec"                                  >
<!ENTITY % relheader   "relheader"                                   >
<!ENTITY % relrow      "relrow"                                      >
<!ENTITY % reltable    "reltable"                                    >
<!ENTITY % searchtitle "searchtitle"                                 >
<!ENTITY % shortdesc   "shortdesc"                                   >
<!ENTITY % topicmeta   "topicmeta"                                   >
<!ENTITY % topicref    "topicref"                                    >


<!-- ============================================================= -->
<!--                    ENTITY DECLARATIONS FOR ATTRIBUTE VALUES   -->
<!-- ============================================================= -->


<!--                    DATE FORMAT                                -->
<!-- Copied into metaDecl.mod -->
<!--<!ENTITY % date-format  'CDATA'                               >-->


<!-- ============================================================= -->
<!--                    COMMON ATTLIST SETS                        -->
<!-- ============================================================= -->


<!ENTITY % topicref-atts 
             "collection-type 
                        (choice | 
                         family | 
                         sequence | 
                         unordered |
                         -dita-use-conref-target) 
                                  #IMPLIED
              type 
                        CDATA 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  #IMPLIED
              scope 
                        (external | 
                         local | 
                         peer | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              locktitle 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              format 
                        CDATA 
                                  #IMPLIED
              linking 
                        (none | 
                         normal | 
                         sourceonly | 
                         targetonly |
                         -dita-use-conref-target) 
                                  #IMPLIED
              toc 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              print 
                        (no | 
                         printonly | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              search 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              chunk 
                        CDATA 
                                  #IMPLIED
  "
>

<!ENTITY % topicref-atts-no-toc 
             'collection-type 
                        (choice | 
                         family | 
                         sequence | 
                         unordered |
                         -dita-use-conref-target) 
                                  #IMPLIED
              type 
                        CDATA 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  #IMPLIED
              scope 
                        (external | 
                         local | 
                         peer | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              locktitle 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              format 
                        CDATA 
                                  #IMPLIED
              linking 
                        (none | 
                         normal | 
                         sourceonly | 
                         targetonly |
                         -dita-use-conref-target) 
                                  #IMPLIED
              toc 
                        (no | 
                         yes | 
                         -dita-use-conref-target)
                                  "no"
              print 
                        (no | 
                         printonly | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              search 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              chunk 
                        CDATA 
                                  #IMPLIED
  '
>

<!ENTITY % topicref-atts-without-format 
             "collection-type 
                        (choice | 
                         family | 
                         sequence | 
                         unordered |
                         -dita-use-conref-target) 
                                  #IMPLIED
              type 
                        CDATA 
                                  #IMPLIED
              processing-role
                        (normal |
                         resource-only |
                         -dita-use-conref-target)
                                  #IMPLIED
              scope 
                        (external | 
                         local | 
                         peer | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              locktitle 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              linking 
                        (none | 
                         normal | 
                         sourceonly | 
                         targetonly |
                         -dita-use-conref-target) 
                                  #IMPLIED
              toc 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              print 
                        (no | 
                         printonly | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              search 
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
                                  #IMPLIED
              chunk 
                        CDATA 
                                  #IMPLIED
  "
>

<!-- ============================================================= -->
<!--                    MODULES CALLS                              -->
<!-- ============================================================= -->


<!--                      Content elements common to map and topic -->
<!ENTITY % commonElements 
  SYSTEM "commonElements.mod" 
>%commonElements;

<!--                      MetaData Elements                        -->
<!ENTITY % metaXML 
  SYSTEM "metaDecl.mod" 
>%metaXML;
 
<!-- ============================================================= -->
<!--                    DOMAINS ATTRIBUTE OVERRIDE                 -->
<!-- ============================================================= -->

<!ENTITY included-domains 
  "" 
>
 
<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS                       -->
<!-- ============================================================= -->





<!--                    LONG NAME: Map                             -->
<!ENTITY % map.content
                       "((%title;)?, 
                         (%topicmeta;)?, 
                         (%anchor; |
                          %data.elements.incl; |
                          %navref; |
                          %reltable; |
                          %topicref;)* )"
>
<!ENTITY % map.attributes
             "title 
                        CDATA 
                                  #IMPLIED
              id 
                        ID 
                                  #IMPLIED
              %conref-atts;
              anchorref 
                        CDATA 
                                  #IMPLIED
              outputclass 
                        CDATA 
                                  #IMPLIED
              %localization-atts;
              %topicref-atts;
              %select-atts;"
>
<!ELEMENT map    %map.content;>
<!ATTLIST map    
              %map.attributes;
              %arch-atts;
              domains 
                        CDATA 
                                  "&included-domains;" 
>



<!--                    LONG NAME: Navigation Reference            -->
<!ENTITY % navref.content
                       "EMPTY"
>
<!ENTITY % navref.attributes
             "%univ-atts;
              keyref 
                        CDATA 
                                  #IMPLIED
              mapref 
                        CDATA 
                                  #IMPLIED 
              outputclass 
                        CDATA 
                                  #IMPLIED
">
<!ELEMENT navref    %navref.content;>
<!ATTLIST navref    %navref.attributes;>


<!--                    LONG NAME: Topic Reference                 -->
<!ENTITY % topicref.content
                       "((%topicmeta;)?, 
                         (%anchor; | 
                          %data.elements.incl; |
                          %navref; | 
                          %topicref;)* )"
>
<!ENTITY % topicref.attributes
             "navtitle 
                        CDATA 
                                  #IMPLIED
              href 
                        CDATA 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED
              keys 
                        CDATA 
                                  #IMPLIED
              query 
                        CDATA 
                                  #IMPLIED
              copy-to 
                        CDATA 
                                  #IMPLIED
              outputclass 
                        CDATA 
                                  #IMPLIED
              %topicref-atts;
              %univ-atts;"
>
<!ELEMENT topicref    %topicref.content;>
<!ATTLIST topicref    %topicref.attributes;>


<!--                    LONG NAME: Anchor                          -->
<!ENTITY % anchor.content
                       "EMPTY"
>
<!ENTITY % anchor.attributes
             "outputclass 
                        CDATA 
                                  #IMPLIED
              %localization-atts;
              id 
                        ID 
                                  #REQUIRED 
              %conref-atts;
              %select-atts;"
>
<!ELEMENT anchor    %anchor.content;>
<!ATTLIST anchor    %anchor.attributes;>



<!--                    LONG NAME: Relationship Table              -->
<!ENTITY % reltable.content
                       "((%title;)?,
                         (%topicmeta;)?, 
                         (%relheader;)?, 
                         (%relrow;)+)"
>
<!ENTITY % reltable.attributes
             "title 
                        CDATA 
                                  #IMPLIED
              outputclass 
                        CDATA 
                                  #IMPLIED
              %topicref-atts-no-toc;
              %univ-atts;"
>
<!ELEMENT reltable    %reltable.content;>
<!ATTLIST reltable    %reltable.attributes;>



<!--                    LONG NAME: Relationship Header             -->
<!ENTITY % relheader.content
                       "(%relcolspec;)+"
>
<!ENTITY % relheader.attributes
             "%univ-atts;"
>
<!ELEMENT relheader    %relheader.content;>
<!ATTLIST relheader    %relheader.attributes;>
 


<!--                    LONG NAME: Relationship Column Specification
                                                                   -->
<!ENTITY % relcolspec.content
                       "((%title;)?,
                         (%topicmeta;)?,
                         (%topicref;)*)
">
<!ENTITY % relcolspec.attributes
             "outputclass 
                        CDATA 
                                  #IMPLIED
              %topicref-atts;
              %univ-atts;"
>
<!ELEMENT relcolspec    %relcolspec.content;>
<!ATTLIST relcolspec    %relcolspec.attributes;>



<!--                    LONG NAME: Relationship Table Row          -->
<!ENTITY % relrow.content
                       "(%relcell;)*"
>
<!ENTITY % relrow.attributes
             "outputclass 
                        CDATA 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT relrow    %relrow.content;>
<!ATTLIST relrow    %relrow.attributes;>



<!--                    LONG NAME: Relationship Table Cell         -->
<!ENTITY % relcell.content
                       "(%topicref; |
                         %data.elements.incl;)*
">
<!ENTITY % relcell.attributes
             "outputclass 
                        CDATA 
                                  #IMPLIED
              %topicref-atts;
              %univ-atts;"
>
<!ELEMENT relcell    %relcell.content;>
<!ATTLIST relcell    %relcell.attributes;>



<!--                    LONG NAME: Topic Metadata                  -->
<!ENTITY % topicmeta.content
                       "((%navtitle;)?,
                         (%linktext;)?, 
                         (%searchtitle;)?, 
                         (%shortdesc;)?, 
                         (%author;)*, 
                         (%source;)?, 
                         (%publisher;)?, 
                         (%copyright;)*, 
                         (%critdates;)?, 
                         (%permissions;)?, 
                         (%metadata;)*, 
                         (%audience;)*, 
                         (%category;)*, 
                         (%keywords;)*, 
                         (%prodinfo;)*, 
                         (%othermeta;)*, 
                         (%resourceid;)*, 
                         (%data.elements.incl; | 
                          %foreign.unknown.incl;)*)"
>
<!ENTITY % topicmeta.attributes
             "lockmeta 
                       (no | 
                        yes | 
                        -dita-use-conref-target) 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT topicmeta    %topicmeta.content;>
<!ATTLIST topicmeta    %topicmeta.attributes;>



<!--                    LONG NAME: Link Text                       -->
<!ENTITY % linktext.content
                       "(%words.cnt; |
                         %ph;)*"
>
<!ENTITY % linktext.attributes
             "outputclass 
                        CDATA 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT linktext    %linktext.content;>
<!ATTLIST linktext    %linktext.attributes;>



<!--                    LONG NAME: Search Title                    -->
<!ENTITY % searchtitle.content
                       "(%words.cnt;)*"
>
<!ENTITY % searchtitle.attributes
             "outputclass 
                        CDATA 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT searchtitle    %searchtitle.content;>
<!ATTLIST searchtitle    %searchtitle.attributes;>



<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->


<!ATTLIST map         %global-atts;  class CDATA "- map/map "        >
<!ATTLIST navref      %global-atts;  class CDATA "- map/navref "     >
<!ATTLIST topicref    %global-atts;  class CDATA "- map/topicref "   >
<!ATTLIST anchor      %global-atts;  class CDATA "- map/anchor "     >
<!ATTLIST reltable    %global-atts;  class CDATA "- map/reltable "   >
<!ATTLIST relheader   %global-atts;  class CDATA "- map/relheader "  >
<!ATTLIST relcolspec  %global-atts;  class CDATA "- map/relcolspec " >
<!ATTLIST relrow      %global-atts;  class CDATA "- map/relrow "     >
<!ATTLIST relcell     %global-atts;  class CDATA "- map/relcell "    >
<!ATTLIST topicmeta   %global-atts;  class CDATA "- map/topicmeta "  >
<!ATTLIST linktext    %global-atts;  class CDATA "- map/linktext "   >
<!ATTLIST searchtitle %global-atts;  class CDATA "- map/searchtitle ">

<!-- Shortdesc in topic uses topic/shortdesc so this one must be 
     included, even though the element is common. -->
<!ATTLIST shortdesc   %global-atts;  class CDATA "- map/shortdesc "  >


<!-- ================== End DITA Map ============================= -->