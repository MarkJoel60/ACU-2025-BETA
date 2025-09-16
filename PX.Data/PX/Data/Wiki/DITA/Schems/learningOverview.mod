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
<!--                   SPECIALIZATION OF DECLARED ELEMENTS         -->
<!-- ============================================================= -->

<!ENTITY % learningOverview "learningOverview">
<!ENTITY % learningOverviewbody "learningOverviewbody">


<!ENTITY % learningOverview-info-types "no-topic-nesting">
<!ENTITY included-domains 
                        "" >

<!ENTITY % learningOverview.content
                       "((%title;),
                         (%titlealts;)?,
                         (%shortdesc; |
                          %abstract;)?, 
                         (%prolog;)?,
                         (%learningOverviewbody;), 
                         (%related-links;)?,
                         (%learningOverview-info-types;)* )"
>
<!ENTITY % learningOverview.attributes
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
<!ELEMENT learningOverview    %learningOverview.content;>
<!ATTLIST learningOverview    
              %learningOverview.attributes;
              %arch-atts;
              domains 
                        CDATA
                                  "&included-domains;"
>


<!ENTITY % learningOverviewbody.content
                       "((%lcIntro;)?,
                         (%lcAudience;)*,
                         (%lcDuration;)?,
                         (%lcPrereqs;)?,
                         (%lcObjectives;)?,
                         (%lcResources;)?,
                         (%section;)*)  "
>
<!ENTITY % learningOverviewbody.attributes
             "%univ-atts;
              outputclass
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT learningOverviewbody    %learningOverviewbody.content;>
<!ATTLIST learningOverviewbody    %learningOverviewbody.attributes;>


<!--specialization attributes-->

<!ATTLIST learningOverview        %global-atts; class CDATA "- topic/topic learningBase/learningBase     learningOverview/learningOverview ">
<!ATTLIST learningOverviewbody    %global-atts; class CDATA "- topic/body  learningBase/learningBasebody learningOverview/learningOverviewbody ">
