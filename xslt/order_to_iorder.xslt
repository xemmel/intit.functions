<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:template match="Input">
        <Order xmlns="https://order.dk">
            <Id>
                <xsl:value-of select="id" />
            </Id>
        </Order>
    </xsl:template>
</xsl:stylesheet>