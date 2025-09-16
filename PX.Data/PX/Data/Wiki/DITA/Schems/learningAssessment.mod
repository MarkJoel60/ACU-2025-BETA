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

<!ENTITY % learningAssessment     "learningAssessment">
<!ENTITY % learningAssessmentbody "learningAssessmentbody">

<!ENTITY % learningAssessment-info-types "no-topic-nesting">

<!ENTITY included-domains 
  ""
>

<!--                    LONG NAME: Learning Assessment             -->
<!ENTITY % learningAssessment.content
                        "((%title;),
                          (%titlealts;)?,
                          (%shortdesc; | 
                           %abstract;)?,
                          (%prolog;)?,
                          (%learningAssessmentbody;),
                          (%related-links;)?,
                          (%learningAssessment-info-types;)* )"
>
<!ENTITY % learningAssessment.attributes
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
<!ELEMENT learningAssessment    %learningAssessment.content;>
<!ATTLIST learningAssessment
              %learningAssessment.attributes;
              %arch-atts;
              domains 
                        CDATA
                                  "&included-domains;"
>

<!--                    LONG NAME: Learning Assessment Body        -->
<!ENTITY % learningAssessmentbody.content
                        "((%lcIntro;)?,
                          (%lcObjectives;)?,
                          (%lcDuration;)?,
                          (%lcInteraction;)*,
                          (%section;)*,
                          (%lcSummary;)?)"
>
<!ENTITY % learningAssessmentbody.attributes
             "%univ-atts;
              outputclass
                        CDATA
                                  #IMPLIED"
>
<!ELEMENT learningAssessmentbody   %learningAssessmentbody.content;>
<!ATTLIST learningAssessmentbody   %learningAssessmentbody.attributes;>

<!-- ============================================================= -->
<!--                    SPECIALIZATION ATTRIBUTE DECLARATIONS      -->
<!-- ============================================================= -->
 
<!ATTLIST learningAssessment        %global-atts; class CDATA "- topic/topic learningBase/learningBase     learningAssessment/learningAssessment ">
<!ATTLIST learningAssessmentbody    %global-atts; class CDATA "- topic/body  learningBase/learningBasebody learningAssessment/learningAssessmentbody ">




