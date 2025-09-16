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
<!--                    ELEMENT NAME ENTITIES                      -->
<!-- ============================================================= -->


<!ENTITY % date-format 
 "CDATA"
>

<!-- ============================================================= -->
<!--                    ELEMENT DECLARATIONS                       -->
<!-- ============================================================= -->

<!--                    LONG NAME: Author                          -->
<!ENTITY % author.content
                       "(%words.cnt;)*"
>
<!-- 20080128: Removed enumeration for @type for DITA 1.2. Previous values:
               contributor, creator, -dita-use-conref-target           -->
<!ENTITY % author.attributes
             "%univ-atts;
              href 
                        CDATA 
                                  #IMPLIED
              format 
                        CDATA 
                                  #IMPLIED
              scope 
                       (external | 
                        local | 
                        peer | 
                        -dita-use-conref-target) 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED
              type 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT author    %author.content;> 
<!ATTLIST author    %author.attributes;>

<!--                     LONG NAME: Source                         -->
<!ENTITY % source.content
                       "(%words.cnt; |
                         %ph;)*"
>
<!ENTITY % source.attributes
             "%univ-atts;
              href 
                        CDATA 
                                  #IMPLIED
              format 
                        CDATA 
                                  #IMPLIED
              type 
                        CDATA 
                                  #IMPLIED
              scope 
                       (external | 
                        local | 
                        peer | 
                        -dita-use-conref-target) 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT source    %source.content;>
<!ATTLIST source    %source.attributes;>



<!--                    LONG NAME: Publisher                       -->
<!ENTITY % publisher.content
                       "(%words.cnt;)*"
>
<!ENTITY % publisher.attributes
             "href 
                        CDATA 
                                  #IMPLIED
              format 
                        CDATA 
                                  #IMPLIED
              type 
                        CDATA 
                                  #IMPLIED
              scope 
                       (external | 
                        local | 
                        peer | 
                        -dita-use-conref-target) 
                                  #IMPLIED
              keyref 
                        CDATA 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT publisher    %publisher.content;>
<!ATTLIST publisher    %publisher.attributes;>

<!--                    LONG NAME: Copyright                       -->
<!ENTITY % copyright.content
                       "((%copyryear;)+, 
                         (%copyrholder;))"
>
<!-- 20080128: Removed enumeration for @type for DITA 1.2. Previous values:
               primary, secondary, -dita-use-conref-target           -->
<!ENTITY % copyright.attributes
             "%univ-atts;
              type 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT copyright    %copyright.content;> 
<!ATTLIST copyright    %copyright.attributes;>

<!--                    LONG NAME: Copyright Year                  -->
<!ENTITY % copyryear.content
                       "EMPTY"
>
<!ENTITY % copyryear.attributes
             "year 
                        %date-format; 
                                  #REQUIRED
              %univ-atts;"
>
<!ELEMENT copyryear    %copyryear.content;>
<!ATTLIST copyryear    %copyryear.attributes;>



<!--                    LONG NAME: Copyright Holder                -->
<!ENTITY % copyrholder.content
                       "(%words.cnt;)*"
>
<!ENTITY % copyrholder.attributes
             "%univ-atts;"
>
<!ELEMENT copyrholder    %copyrholder.content;>
<!ATTLIST copyrholder    %copyrholder.attributes;>



<!--                    LONG NAME: Critical Dates                  -->
<!ENTITY % critdates.content
                       "((%created;)?, 
                         (%revised;)*)"
>
<!ENTITY % critdates.attributes
             "%univ-atts;"
>
<!ELEMENT critdates    %critdates.content;>
<!ATTLIST critdates    %critdates.attributes;>



<!--                    LONG NAME: Created Date                    -->
<!ENTITY % created.content
                       "EMPTY"
>
<!ENTITY % created.attributes
             "date 
                        %date-format; 
                                  #REQUIRED
              golive 
                        %date-format; 
                                  #IMPLIED
              expiry 
                        %date-format; 
                                  #IMPLIED 
              %univ-atts;"
>
<!ELEMENT created    %created.content;>
<!ATTLIST created    %created.attributes;>



<!--                    LONG NAME: Revised Date                    -->
<!ENTITY % revised.content
                       "EMPTY"
>
<!ENTITY % revised.attributes
             "modified 
                        %date-format; 
                                  #REQUIRED
              golive 
                        %date-format; 
                                  #IMPLIED
              expiry 
                        %date-format; 
                                  #IMPLIED 
              %univ-atts;"
>
<!ELEMENT revised    %revised.content;>
<!ATTLIST revised    %revised.attributes;>


<!--                    LONG NAME: Permissions                     -->
<!ENTITY % permissions.content
                       "EMPTY"
>
<!-- 20080128: Removed enumeration for @type for DITA 1.2. Previous values:
               all, classified, entitled, internal, -dita-use-conref-target -->
<!ENTITY % permissions.attributes
             "%univ-atts;
              view 
                        CDATA 
                                  #REQUIRED"
>
<!ELEMENT permissions    %permissions.content;>  
<!ATTLIST permissions    %permissions.attributes;>

<!--                    LONG NAME: Category                        -->
<!ENTITY % category.content
                       "(%words.cnt;)*"
>
<!ENTITY % category.attributes
             "%univ-atts;"
>
<!ELEMENT category    %category.content;>
<!ATTLIST category    %category.attributes;>


<!--                    LONG NAME: Metadata                        -->
<!ENTITY % metadata.content
                       "((%audience;)*, 
                         (%category;)*, 
                         (%keywords;)*,
                         (%prodinfo;)*, 
                         (%othermeta;)*, 
                         (%data.elements.incl; |
                          %foreign.unknown.incl;)*)"
>
<!ENTITY % metadata.attributes
             "%univ-atts; 
              mapkeyref 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT metadata    %metadata.content;>
<!ATTLIST metadata    %metadata.attributes;>


<!--                    LONG NAME: Audience                        -->
<!ENTITY % audience.content
                       "EMPTY"
>
<!-- 20080128: Removed enumerations for DITA 1.2. Previous values:
         @type: administrator, executive, other, purchaser, programmer, 
                services, user, -dita-use-conref-target
         @job: administering, customizing, evaluating, installing,
               maintaining, migrating, other, planning, programming,
               troubleshooting, using, -dita-use-conref-target
         @experiencelevel: expert, general, novice, -dita-use-conref-target -->
<!ENTITY % audience.attributes
             "type 
                        CDATA 
                                  #IMPLIED
              othertype 
                        CDATA 
                                  #IMPLIED
              job 
                        CDATA
                                  #IMPLIED
              otherjob 
                        CDATA 
                                  #IMPLIED
              experiencelevel
                        CDATA 
                                  #IMPLIED
              name 
                        NMTOKEN 
                                  #IMPLIED
              %univ-atts;"
>
<!ELEMENT audience    %audience.content;> 
<!ATTLIST audience    %audience.attributes;>

<!--                    LONG NAME: Keywords                        -->
<!ENTITY % keywords.content
                       "(%indexterm; | 
                         %keyword;)*"
>
<!ENTITY % keywords.attributes
             "%univ-atts;"
>
<!ELEMENT keywords    %keywords.content;>
<!ATTLIST keywords    %keywords.attributes;>



<!--                    LONG NAME: Product Information             -->
<!ENTITY % prodinfo.content
                       "((%prodname;), 
                         (%vrmlist;),
                         (%brand; | 
                          %component; | 
                          %featnum; | 
                          %platform; | 
                          %prognum; | 
                          %series;)* )"
>
<!ENTITY % prodinfo.attributes
             "%univ-atts;"
>
<!ELEMENT prodinfo    %prodinfo.content;>
<!ATTLIST prodinfo    %prodinfo.attributes;>



<!--                    LONG NAME: Product Name                    -->
<!ENTITY % prodname.content
                       "(%words.cnt;)*"
>
<!ENTITY % prodname.attributes
             "%univ-atts;"
>
<!ELEMENT prodname    %prodname.content;>
<!ATTLIST prodname    %prodname.attributes;>



<!--                    LONG NAME: Version Release and Modification
                                   List                            -->
<!ENTITY % vrmlist.content
                       "(%vrm;)+"
>
<!ENTITY % vrmlist.attributes
             "%univ-atts;"
>
<!ELEMENT vrmlist    %vrmlist.content;>
<!ATTLIST vrmlist    %vrmlist.attributes;>



<!--                    LONG NAME: Version Release and Modification-->
<!ENTITY % vrm.content
                       "EMPTY"
>
<!ENTITY % vrm.attributes
             "version 
                        CDATA 
                                  #REQUIRED
              release 
                        CDATA 
                                  #IMPLIED
              modification 
                        CDATA 
                                  #IMPLIED 
              %univ-atts;"
>
<!ELEMENT vrm    %vrm.content;>
<!ATTLIST vrm    %vrm.attributes;>

 
<!--                    LONG NAME: Brand                           -->
<!ENTITY % brand.content
                       "(%words.cnt;)*"
>
<!ENTITY % brand.attributes
             "%univ-atts;"
>
<!ELEMENT brand    %brand.content;>
<!ATTLIST brand    %brand.attributes;>



<!--                    LONG NAME: Series                          -->
<!ENTITY % series.content
                       "(%words.cnt;)*"
>
<!ENTITY % series.attributes
             "%univ-atts;"
>
<!ELEMENT series    %series.content;>
<!ATTLIST series    %series.attributes;>



<!--                    LONG NAME: Platform                        -->
<!ENTITY % platform.content
                       "(%words.cnt;)*"
>
<!ENTITY % platform.attributes
             "%univ-atts;"
>
<!ELEMENT platform    %platform.content;>
<!ATTLIST platform    %platform.attributes;>


<!--                    LONG NAME: Program Number                  -->
<!ENTITY % prognum.content
                       "(%words.cnt;)*"
>
<!ENTITY % prognum.attributes
             "%univ-atts;"
>
<!ELEMENT prognum    %prognum.content;>
<!ATTLIST prognum    %prognum.attributes;>



<!--                    LONG NAME: Feature Number                  -->
<!ENTITY % featnum.content
                       "(%words.cnt;)*"
>
<!ENTITY % featnum.attributes
             "%univ-atts;"
>
<!ELEMENT featnum    %featnum.content;>
<!ATTLIST featnum    %featnum.attributes;>



<!--                    LONG NAME: Component                       -->
<!ENTITY % component.content
                       "(%words.cnt;)*"
>
<!ENTITY % component.attributes
             "%univ-atts;"
>
<!ELEMENT component    %component.content;>
<!ATTLIST component    %component.attributes;>



<!--                    LONG NAME: Other Metadata                  -->
<!--                    NOTE: needs to be HTML-equiv, at least     -->
<!ENTITY % othermeta.content
                       "EMPTY"
>
<!ENTITY % othermeta.attributes
             "name 
                        CDATA 
                                  #REQUIRED
              content 
                        CDATA 
                                  #REQUIRED
              translate-content
                        (no | 
                         yes | 
                         -dita-use-conref-target) 
              #IMPLIED
              %univ-atts;"
>
<!ELEMENT othermeta    %othermeta.content;>
<!ATTLIST othermeta    %othermeta.attributes;>



<!--                    LONG NAME: Resource Identifier             -->
<!ENTITY % resourceid.content
                       "EMPTY"
>
<!ENTITY % resourceid.attributes
             "%select-atts;
              %localization-atts;
              id 
                        CDATA 
                                  #REQUIRED
              %conref-atts;
              appname 
                        CDATA 
                                  #IMPLIED"
>
<!ELEMENT resourceid    %resourceid.content;>
<!ATTLIST resourceid    %resourceid.attributes;>



<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->
 

<!ATTLIST author      %global-atts;  class CDATA "- topic/author "      >
<!ATTLIST source      %global-atts;  class CDATA "- topic/source "      >
<!ATTLIST publisher   %global-atts;  class CDATA "- topic/publisher "   >
<!ATTLIST copyright   %global-atts;  class CDATA "- topic/copyright "   >
<!ATTLIST copyryear   %global-atts;  class CDATA "- topic/copyryear "   >
<!ATTLIST copyrholder %global-atts;  class CDATA "- topic/copyrholder " >
<!ATTLIST critdates   %global-atts;  class CDATA "- topic/critdates "   >
<!ATTLIST created     %global-atts;  class CDATA "- topic/created "     >
<!ATTLIST revised     %global-atts;  class CDATA "- topic/revised "     >
<!ATTLIST permissions %global-atts;  class CDATA "- topic/permissions " >
<!ATTLIST category    %global-atts;  class CDATA "- topic/category "    >
<!ATTLIST metadata    %global-atts;  class CDATA "- topic/metadata "   >
<!ATTLIST audience    %global-atts;  class CDATA "- topic/audience "    >
<!ATTLIST keywords    %global-atts;  class CDATA "- topic/keywords "    >
<!ATTLIST prodinfo    %global-atts;  class CDATA "- topic/prodinfo "    >
<!ATTLIST prodname    %global-atts;  class CDATA "- topic/prodname "    >
<!ATTLIST vrmlist     %global-atts;  class CDATA "- topic/vrmlist "     >
<!ATTLIST vrm         %global-atts;  class CDATA "- topic/vrm "         >
<!ATTLIST brand       %global-atts;  class CDATA "- topic/brand "       >
<!ATTLIST series      %global-atts;  class CDATA "- topic/series "      >
<!ATTLIST platform    %global-atts;  class CDATA "- topic/platform "    >
<!ATTLIST prognum     %global-atts;  class CDATA "- topic/prognum "     >
<!ATTLIST featnum     %global-atts;  class CDATA "- topic/featnum "     >
<!ATTLIST component   %global-atts;  class CDATA "- topic/component "   >
<!ATTLIST othermeta   %global-atts;  class CDATA "- topic/othermeta "   >
<!ATTLIST resourceid  %global-atts;  class CDATA "- topic/resourceid "  >

<!-- ================== End Metadata  ================================ -->